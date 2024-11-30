using System;
using Microsoft.SPOT;

namespace Mjunit.Clients
{
    public class DebugTestClient : ITestClient
    {
        public void OnTestRunComplete(object sender, TestRunEventHandlerArgs args)
        {
            Debug.Print("Test run complete.");
            Debug.Print("Outcome: " + GetOutcomeText(args.Result.Outcome));
            Debug.Print("Tests run: " + args.Result.NumberOfTests);
            Debug.Print("Tests passed: " + args.Result.NumberOfTestsPassed);
            Debug.Print("Tests failed: " + args.Result.NumberOfTestsFailed);
        }

        public void OnSingleTestComplete(object sender, TestRunEventHandlerArgs args)
        {
            Debug.Print("Test: " + args.Result.Name + " - " + GetOutcomeText(args.Result.Outcome));
        }

        public void OnTestRunStart(object sender, TestRunEventHandlerArgs args)
        {
            Debug.Print("Starting test run...");
        }

        private static string GetOutcomeText(TestOutcome outcome)
        {
            if (outcome == TestOutcome.Pass)
            {
                return "Pass";
            }

            if (outcome == TestOutcome.Fail)
            {
                return "Fail";
            }

            return "Unknown";
        }
    }
}
