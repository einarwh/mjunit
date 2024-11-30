using System;
using System.Threading;

using Gadgeteer.Modules.GHIElectronics;

using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Mjunit.Clients.GHI
{
    public class DisplayTestClient : ITestClient
    {
        private readonly Display_T35 _display;

        private readonly Font _font;

        private readonly Bitmap _bitmap;

        private TestOutcome _outcome;

        public DisplayTestClient(Display_T35 display, Font font)
        {
            _display = display;
            _font = font;
            _bitmap = new Bitmap((int)_display.Width, (int)_display.Height);
        }

        public void OnTestRunComplete(object sender, TestRunEventHandlerArgs args)
        {
            _bitmap.Clear();
            DrawOverview(args.Result);
            _bitmap.Flush();
        }

        private void DrawOverview(TestResult r)
        {
            var color = r.Outcome == TestOutcome.Pass ? Colors.Green : Colors.Red;
            string text = r.Name + "\nTests run: " + r.NumberOfTests + "\nTests passed: " + r.NumberOfTestsPassed
                          + "\nTests failed: " + r.NumberOfTestsFailed;
            _bitmap.DrawTextInRect(text, 10, 10, 220, 150, 0, color, _font);
        }

        private void DrawSingle(TestResult r)
        {
            var color = r.Outcome == TestOutcome.Pass ? Colors.Green : Colors.Red;
            string outcomeText = GetOutcomeText(r.Outcome);
            _bitmap.DrawTextInRect(r.Name + " - " + outcomeText, 10, 200, 220, 40, 0, color, _font);
        }


        public void OnSingleTestComplete(object sender, TestRunEventHandlerArgs args)
        {
            var r = args.Result;
            if (_outcome == TestOutcome.Pass)
            {
                _outcome = r.Outcome;
            }

            _bitmap.Clear();
            //DrawOverview(r);
            DrawSingle(r);
            _bitmap.Flush();
            Thread.Sleep(4000);
        }

        private string GetOutcomeText(TestOutcome outcome)
        {
            if (outcome == TestOutcome.Pass)
            {
                return "Pass";
            }

            if (outcome == TestOutcome.Fail)
            {
                return "Fail";
            }

            return string.Empty;
        }

        public void OnTestRunStart(object sender, TestRunEventHandlerArgs args)
        {
            _outcome = TestOutcome.Pass;
            _bitmap.Clear();
        }
    }
}
