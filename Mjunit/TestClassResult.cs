using System;

namespace Mjunit
{
    class TestClassResult : TestGroupResult
    {
        private readonly Type _testClass;

        public TestClassResult(Type testClass)
            : base(testClass.Name)
        {
            _testClass = testClass;
        }

        public Type TestClass
        {
            get
            {
                return _testClass;
            }
        }
    }
}
