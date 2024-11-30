using System;

namespace Mjunit
{
    public class AssertFailedException : Exception
    {
        public AssertFailedException(string message) : base(message)
        {
        }
    }
}