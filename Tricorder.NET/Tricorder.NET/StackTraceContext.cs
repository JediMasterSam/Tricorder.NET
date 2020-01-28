using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.String;

namespace Tricorder.NET
{
    internal sealed class StackTraceContext
    {
        internal StackTraceContext(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            DeclaringType = type.FullName;
        }

        private string DeclaringType { get; }

        internal IEnumerable<StackFrame> GetFrames()
        {
            var stackFrames = new StackTrace(true).GetFrames();

            if (stackFrames == null || stackFrames.Length == 0) yield break;

            foreach (var stackFrame in stackFrames)
            {
                var declaringType = stackFrame?.GetMethod()?.DeclaringType?.FullName;

                if (IsNullOrEmpty(declaringType)) continue;

                var index = declaringType.LastIndexOf('+');

                if (index > 0)
                {
                    declaringType = declaringType.Substring(0, index);
                }

                if (DeclaringType == declaringType)
                {
                    yield return stackFrame;
                }
            }
        }
    }
}