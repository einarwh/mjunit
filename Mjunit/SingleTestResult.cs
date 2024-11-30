using System;
using System.Reflection;

namespace Mjunit
{
    public class SingleTestResult : TestResult
    {
        private readonly MethodInfo _method;

        private readonly TestOutcome _testOutcome;

        public SingleTestResult(MethodInfo method, TestOutcome testOutcome)
        {
            _method = method;
            _testOutcome = testOutcome;
        }

        public MethodInfo Method
        {
            get
            {
                return _method;
            }
        }

        public override string Name
        {
            get
            {
                return _method.Name;
            }
        }

        public override TestOutcome Outcome
        {
            get
            {
                return _testOutcome;
            }
        }

        public override int NumberOfTests
        {
            get
            {
                return 1;
            }
        }

        public override int NumberOfTestsPassed
        {
            get
            {
                return Outcome == TestOutcome.Pass ? 1 : 0;
            }
        }

        public override int NumberOfTestsFailed
        {
            get
            {
                return Outcome == TestOutcome.Fail ? 1 : 0;
            }
        }

        public AssertFailedException AssertFailedException { get; set; }

        public Exception Exception { get; set; }
    }
}