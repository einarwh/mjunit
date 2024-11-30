using System;
using System.Collections;
using System.Reflection;
using System.Threading;

using Microsoft.SPOT;

namespace Mjunit
{
    public class TestRunner
    {
        private Thread _thread;
        private Assembly _assembly;
        private bool _done;

        public event TestRunEventHandler SingleTestComplete;
        public event TestRunEventHandler TestRunStart;
        public event TestRunEventHandler TestRunComplete;

        public TestRunner() {}

        public TestRunner(ITestClient client)
        {
            RegisterClient(client);
        }

        public TestRunner(ArrayList clients)
        {
            foreach (ITestClient c in clients)
            {
                RegisterClient(c);
            }
        }

        public bool Done
        {
            get
            {
                return _done;
            }
        }

        public void RegisterClient(ITestClient client)
        {
            TestRunStart += client.OnTestRunStart;
            SingleTestComplete += client.OnSingleTestComplete;
            TestRunComplete += client.OnTestRunComplete;
        }

        public void Run(Assembly assembly)
        {
            _assembly = assembly;
            _thread = new Thread(DoRun);
            _thread.Start();
        }

        public void Run(Type type)
        {
            Run(Assembly.GetAssembly(type));
        }

        public void DoRun()
        {
            FireCompleteEvent(TestRunStart, null);
            Assembly assembly = _assembly;
            var gr = new TestGroupResult(assembly.FullName);
            try
            {
                var finder = new TestFinder();
                var groups = finder.FindTests(assembly);
                foreach (TestGroup g in groups)
                {
                    gr.AddResult(Run(g));
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.StackTrace);
            }

            FireCompleteEvent(TestRunComplete, gr);
            _done = true;
        }

        private void FireCompleteEvent(TestRunEventHandler handler, TestResult result)
        {
            if (handler != null)
            {
                var args = new TestRunEventHandlerArgs { Result = result };
                handler(this, args);
            }
        }

        public void Cancel()
        {
            _thread.Abort();
        }

        private TestClassResult Run(TestGroup group)
        {
            var result = new TestClassResult(group.TestClass);
            foreach (MethodInfo m in group)
            {
                var r = RunTest(m);
                FireCompleteEvent(SingleTestComplete, r);
                result.AddResult(r);
            }
            return result;
        }

        private SingleTestResult RunTest(MethodInfo m)
        {
            try
            {
                DoRunTest(m);
                return TestPassed(m);
            }
            catch (AssertFailedException ex)
            {
                return TestFailed(m, ex);
            }
            catch (Exception ex)
            {
                return TestFailedWithException(m, ex);
            }
        }

        private SingleTestResult TestFailedWithException(MethodInfo m, Exception ex)
        {
            return new SingleTestResult(m, TestOutcome.Fail) { Exception = ex };
        }

        private SingleTestResult TestFailed(MethodInfo m, AssertFailedException ex)
        {
            return new SingleTestResult(m, TestOutcome.Fail) { AssertFailedException = ex };
        }

        private SingleTestResult TestPassed(MethodInfo m)
        {
            return new SingleTestResult(m, TestOutcome.Pass);
        }

        private void DoRunTest(MethodInfo method)
        {
            Fixture testObj = null;
            try
            {
                testObj = GetInstance(method.DeclaringType);
                testObj.Setup();
                method.Invoke(testObj, new object[0]);
            }
            finally
            {
                if (testObj != null)
                {
                    testObj.Teardown();       
                }
            }
        }

        private Fixture GetInstance(Type testClass)
        {
            var ctor = testClass.GetConstructor(new Type[0]);
            return (Fixture)ctor.Invoke(new object[0]);
        }
    }
}
