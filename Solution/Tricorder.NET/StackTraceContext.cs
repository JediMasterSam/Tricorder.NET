using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.String;

namespace Tricorder.NET
{
    /// <summary>
    /// Represents the stack trace within the context of a specific type.
    /// </summary>
    internal sealed class StackTraceContext
    {
        /// <summary>
        /// Creates a new instance of <see cref="StackTraceContext"/>.
        /// </summary>
        /// <param name="type">The type that defines the context.</param>
        /// <exception cref="ArgumentNullException">The type cannot be null.</exception>
        internal StackTraceContext(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            DeclaringType = type.FullName;
        }

        /// <summary>
        /// The type name that defines the context.
        /// </summary>
        private string DeclaringType { get; }

        /// <summary>
        /// Gets the first <see cref="StackFrame"/> within the current context.
        /// </summary>
        /// <returns>The first <see cref="StackFrame"/> within the current context.</returns>
        internal StackFrame First()
        {
            return GetFrames().FirstOrDefault(IsMatch);
        }

        /// <summary>
        /// Gets every <see cref="StackFrame"/> in the current <see cref="StackTrace"/>.
        /// </summary>
        /// <returns>A collection of <see cref="StackFrame"/> with file information.</returns>
        private static IEnumerable<StackFrame> GetFrames()
        {
            return new StackTrace(true).GetFrames();
        }

        /// <summary>
        /// Does the given <see cref="StackFrame"/> match the current context?
        /// </summary>
        /// <param name="stackFrame">Stack frame.</param>
        /// <returns>True if the stack frame is within the current context; otherwise, false.</returns>
        private bool IsMatch(StackFrame stackFrame)
        {
            var declaringType = stackFrame?.GetMethod()?.DeclaringType?.FullName;

            if (IsNullOrEmpty(declaringType)) return false;

            var index = declaringType.LastIndexOf('+');

            return DeclaringType == (index < 0 ? declaringType : declaringType.Substring(0, index));
        }
    }
}