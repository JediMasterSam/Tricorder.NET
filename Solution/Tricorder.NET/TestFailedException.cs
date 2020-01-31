using System;

namespace Tricorder.NET
{
    /// <summary>
    /// Represents a failed test.
    /// </summary>
    internal sealed class TestFailedException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="TestFailedException"/>.
        /// </summary>
        /// <param name="message">The message to output to the user.</param>
        internal TestFailedException(string message) : base(message)
        {
            StackTrace = " ";
        }

        /// <inheritdoc/>
        public override string StackTrace { get; }
    }
}