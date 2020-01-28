using System;

namespace Tricorder.NET
{
    public sealed class TestFailedException : Exception
    {
        internal TestFailedException(string message) : base(message)
        {
            StackTrace = " ";
        }

        /// <inheritdoc/>
        public override string StackTrace { get; }
    }
}