using System.Collections;

namespace Mjunit
{
    public class TestGroupResult : TestResult
    {
        private delegate int TestCounter(TestResult r); 

        private readonly string _groupName;
        private readonly ArrayList _testResults = new ArrayList();

        public TestGroupResult(string groupName)
        {
            _groupName = groupName;
        }

        public override string Name
        {
            get
            {
                return _groupName;
            }
        }

        public override TestOutcome Outcome
        {
            get
            {
                foreach (TestResult r in _testResults)
                {
                    if (r.Outcome == TestOutcome.Fail)
                    {
                        return TestOutcome.Fail;
                    }
                }

                return TestOutcome.Pass;
            }
        }

        private int GetNumberOfTests(TestCounter counter)
        {
            int result = 0;
            foreach (TestResult r in _testResults)
            {
                result += counter(r);
            }
            return result;
        }

        public override int NumberOfTests
        {
            get
            {
                return GetNumberOfTests(r => r.NumberOfTests);           
            }
        }

        public override int NumberOfTestsPassed
        {
            get
            {
                return GetNumberOfTests(r => r.NumberOfTestsPassed);
            }
        }

        public override int NumberOfTestsFailed
        {
            get
            {
                return GetNumberOfTests(r => r.NumberOfTestsFailed);
            }
        }

        public void AddResult(TestResult testResult)
        {
            _testResults.Add(testResult);
        }
    }
}