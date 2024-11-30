using System;
using System.Collections;
using System.Reflection;

namespace Mjunit
{
    class TestGroup : IEnumerable
    {
        private readonly Type _testClass;
        private readonly ArrayList _testMethods = new ArrayList();

        public TestGroup(Type testClass)
        {
            _testClass = testClass;
            var methods = _testClass.GetMethods(BindingFlags.Public);
            for (int j = 0; j < methods.Length; j++)
            {
                var m = methods[j];
                var ret = m.ReturnType;
                if (m.Name.Substring(0, 4) == "Test" && ret == typeof(void))
                {
                    _testMethods.Add(m);
                }
            }
        }

        public Type TestClass
        {
            get
            {
                return _testClass;
            }
        }

        public int NumberOfTests
        {
            get
            {
                return _testMethods.Count;
            }
        }

        public void AddTestMethod(MethodInfo method)
        {
            _testMethods.Add(method);
        }

        public IEnumerator GetEnumerator()
        {
            return _testMethods.GetEnumerator();
        }
    }
}
