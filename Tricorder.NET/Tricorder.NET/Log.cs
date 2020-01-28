using System.Collections.Generic;
using static System.String;

namespace Tricorder.NET
{
    internal sealed class Log
    {
        internal Log(bool onlyFailures)
        {
            Assertions = new List<string>();
            OnlyFailures = onlyFailures;
        }

        private List<string> Assertions { get; }

        private bool OnlyFailures { get; }

        private int Count { get; set; }

        private int Failures { get; set; }

        public static implicit operator bool(Log log)
        {
            return log.Failures == 0;
        }

        public override string ToString()
        {
            return $"\nAssertions: {Count}, Passed: {Count - Failures}, Failed: {Failures}\n{Join("\n", Assertions)}";
        }

        internal bool Add(Assertion assertion)
        {
            if (!assertion)
            {
                Assertions.Add(assertion.ToString());
                Failures++;
            }
            else if (!OnlyFailures)
            {
                Assertions.Add(assertion.ToString());
            }

            Count++;

            return assertion;
        }
    }
}