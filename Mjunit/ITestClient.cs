using Microsoft.SPOT;

namespace Mjunit
{
    public interface ITestClient
    {
        void OnTestRunComplete(object sender, TestRunEventHandlerArgs args);

        void OnSingleTestComplete(object sender, TestRunEventHandlerArgs args);

        void OnTestRunStart(object sender, TestRunEventHandlerArgs args);
    }
}
