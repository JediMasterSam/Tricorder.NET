using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace Tricorder.NET
{
    internal sealed class Log : IEnumerable<Assertion>
    {
        internal Log(bool onlyFailures)
        {
            Assertions = new List<string>();
            OnlyFailures = onlyFailures;
        }

        internal int Count { get; private set; }

        internal int Failures { get; private set; }

        private List<string> Assertions { get; }

        private bool OnlyFailures { get; }

        public static implicit operator bool(Log log)
        {
            return log.Failures == 0;
        }

        public IEnumerator<Assertion> GetEnumerator()
        {
            return Assertions.OfType<Assertion>().GetEnumerator();
        }

        public override string ToString()
        {
            return $"\nAssertions: {Count}, Passed: {Count - Failures}, Failed: {Failures}\n{Join("\n", Assertions)}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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