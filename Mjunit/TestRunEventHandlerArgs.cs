namespace Mjunit
{
    public delegate void TestRunEventHandler(object sender, TestRunEventHandlerArgs args);

    public class TestRunEventHandlerArgs
    {
        public TestResult Result { get; set; }
    }
}