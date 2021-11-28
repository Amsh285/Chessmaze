using System;

namespace Chessmaze
{
    public sealed class AssertionException : Exception
    {
        public AssertionException(string message)
            : base(message)
        {
        }
    }
}
