using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace Tricorder.NET
{
    /// <summary>
    /// Represents a collection of <see cref="Assertion"/>.
    /// </summary>
    internal sealed class Log : IEnumerable<Assertion>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Log"/>.
        /// </summary>
        /// <param name="onlyFailures">Should only failed assertions be saved?</param>
        internal Log(bool onlyFailures)
        {
            Assertions = new List<Assertion>();
            OnlyFailures = onlyFailures;
        }

        /// <summary>
        /// The total number of assertions received.
        /// </summary>
        internal int Count { get; private set; }

        /// <summary>
        /// The total number of failed assertions.
        /// </summary>
        internal int Failures { get; private set; }

        /// <summary>
        /// Should only failed assertions be saved?
        /// </summary>
        internal bool OnlyFailures { get; }

        /// <summary>
        /// Collection of saved assertions.
        /// </summary>
        private List<Assertion> Assertions { get; }

        /// <summary>
        /// Implicitly converts the given <see cref="Log"/> to its state.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <returns>True if the log does not contain any failures; otherwise, false.</returns>
        public static implicit operator bool(Log log)
        {
            return log.Failures == 0;
        }

        /// <inheritdoc/>
        public IEnumerator<Assertion> GetEnumerator()
        {
            return Assertions.OfType<Assertion>().GetEnumerator();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Assertions: {Count}, Passed: {Count - Failures}, Failed: {Failures}\n{Join("\n", Assertions)}";
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds the given <see cref="Assertion"/> to the current <see cref="Log"/>.
        /// </summary>
        /// <param name="assertion">Assertion.</param>
        /// <returns>The state of the assertion.</returns>
        internal bool Add(Assertion assertion)
        {
            if (!assertion)
            {
                Assertions.Add(assertion);
                Failures++;
            }
            else if (!OnlyFailures)
            {
                Assertions.Add(assertion);
            }

            Count++;

            return assertion;
        }
    }
}