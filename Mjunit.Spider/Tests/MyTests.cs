using System;
using Microsoft.SPOT;

namespace Mjunit.Spider.Tests
{
    public class MyTests : Fixture
    {
        public void Test_Foo()
        {
            Debug.Print("Running Foo test.");
        }

        public void Test_Bar()
        {
            Debug.Print("Running Bar test.");
            Assert.Fail("Total failure.");
        }

        public void Test_Baz()
        {
            Debug.Print("Running Baz test.");
        }
    }
}
