using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        internal StackFrame First()
        {
            return GetFrames().FirstOrDefault(IsMatch);
        }

        private static IEnumerable<StackFrame> GetFrames()
        {
            return new StackTrace(true).GetFrames();
        }

        private bool IsMatch(StackFrame stackFrame)
        {
            var declaringType = stackFrame?.GetMethod()?.DeclaringType?.FullName;

            if (IsNullOrEmpty(declaringType)) return false;

            var index = declaringType.LastIndexOf('+');

            return DeclaringType == (index < 0 ? declaringType : declaringType.Substring(0, index));
        }
    }
}