namespace Mjunit
{
    public static class Assert
    {
        public static void Fail(string message)
        {
            throw new AssertFailedException(message);
        }

        public static void IsTrue(bool value)
        {
            AreEqual(true, value);
        }

        public static void IsFalse(bool value)
        {
            AreEqual(false, value);
        }

        public static void AreEqual(bool expected, bool actual)
        {
            if (expected != actual)
            {
                throw new AssertFailedException("Expected '" + expected + "', but found '" + actual + "'.");
            }
        }

        public static void AreEqual(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new AssertFailedException("Expected '" + expected + "', but found '" + actual + "'.");
            }            
        }

        public static void AreEqual(object expected, object actual)
        {
            if (!EvaluateAreEqual(expected, actual))
            {
                throw new AssertFailedException("Expected '" + expected + "', but found '" + actual + "'.");
            }
        }

        private static bool EvaluateAreEqual(object expected, object actual)
        {
            if (expected == null)
            {
                return actual == null;
            }

            return expected.Equals(actual);
        }
    }
}
