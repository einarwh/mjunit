using Gadgeteer.Modules.GHIElectronics;

using Microsoft.SPOT.Presentation.Media;

namespace Mjunit.Clients.GHI
{
    public class LedTestClient : ITestClient
    {
        private readonly MulticolorLed _led;

        private bool _hasFailed;

        public LedTestClient(MulticolorLed led)
        {
            _led = led;
            Init();
        }

        public void Init()
        {
            _led.TurnOff();
            _hasFailed = false;
        }

        public void OnTestRunStart(object sender, TestRunEventHandlerArgs args)
        {
            Init();
        }

        public void OnTestRunComplete(object sender, TestRunEventHandlerArgs args)
        {
            OnAnyTestComplete(sender, args);
        }

        private void OnAnyTestComplete(object sender, TestRunEventHandlerArgs args)
        {
            if (args.Result.Outcome == TestOutcome.Fail)
            {
                _led.BlinkRepeatedly(Colors.Red);
                _hasFailed = true;
            }

            if (!_hasFailed)
            {
                _led.BlinkRepeatedly(Colors.Green);
            }
        }

        public void OnSingleTestComplete(object sender, TestRunEventHandlerArgs args)
        {
            OnAnyTestComplete(sender, args);
        }
    }
}