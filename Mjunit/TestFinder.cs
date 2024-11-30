using System;
using System.Collections;
using System.Reflection;

using Microsoft.SPOT;

namespace Mjunit
{
    public class TestFinder
    {
        public ArrayList FindTests(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var fixtures = GetTestFixtures(types);
            var groups = GetTestGroups(fixtures);
            return groups;
        }

        private ArrayList GetTestGroups(ArrayList fixtures)
        {
            var result = new ArrayList();
            foreach (Type t in fixtures)
            {
                var g = CreateTestGroup(t);
                if (g.NumberOfTests > 0)
                {
                    result.Add(g);                    
                }
            }
            return result;
        }

        private TestGroup CreateTestGroup(Type testClass)
        {
            var group = new TestGroup(testClass);
            var methods = testClass.GetMethods();

            for (int i = 0; i < methods.Length; i++)
            {
                var m = methods[i];
                if (m.ReturnType == typeof(void) && m.Name.Substring(0, 4) == "Test")
                {
                    group.AddTestMethod(m);
                }
            }
            return group;
        }

        private ArrayList GetTestFixtures(Type[] types)
        {
            var result = new ArrayList();
            for (int i = 0; i < types.Length; i++)
            {
                var t = types[i];
                if (t.IsSubclassOf(typeof(Fixture)))
                {
                    result.Add(t);
                }
            }
            return result;
        }
    }
}
