namespace Mjunit
{
    public abstract class TestResult
    {
        public abstract string Name { get; }

        public abstract TestOutcome Outcome { get; }

        public abstract int NumberOfTests { get; }

        public abstract int NumberOfTestsPassed { get; }

        public abstract int NumberOfTestsFailed { get; }
    }
}