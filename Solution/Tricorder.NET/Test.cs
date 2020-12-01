using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace Tricorder.NET
{
    /// <summary>
    /// Represents an assorted series of assertions.
    /// </summary>
    public abstract class Test
    {
        /// <summary>
        /// Creates a new instance of <see cref="Test"/>.
        /// </summary>
        protected Test()
        {
            Log = new Log(false);
            Context = new StackTraceContext(GetType());
        }

        /// <summary>
        /// Collection of assertions used during the current test.
        /// </summary>
        internal Log Log { get; private set; }

        /// <summary>
        /// The stack trace context of the current type.
        /// </summary>
        private StackTraceContext Context { get; }

        /// <summary>
        /// Has <see cref="Cleanup"/> been run?
        /// </summary>
        private bool Cleaned { get; set; }

        /// <summary>
        /// Analyzes the log to determine the outcome of the test. 
        /// </summary>
        /// <exception cref="TestFailedException">A logged assertion failed.</exception>
        [TestCleanup]
        public void Cleanup()
        {
            if (Cleaned) return;

            Cleaned = true;

            if (!Log)
            {
                throw new TestFailedException(Log.ToString());
            }

            WriteLine(Log.ToString());
        }

        /// <summary>
        /// Only failed assertions will be logged.  If the current log is not empty, all passed assertions will be removed.
        /// </summary>
        protected void LogOnlyFailures()
        {
            var log = new Log(true);

            foreach (var assertion in Log)
            {
                log.Add(assertion);
            }

            Log = log;
        }

        /// <summary>
        /// Are the given expected and actual values equal?
        /// </summary>
        /// <param name="expected">Expected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are equal; otherwise, false.</returns>
        protected bool AreEqual<TValue>(TValue expected, TValue actual)
        {
            return Log.Add(Assertion.AreEqual(expected, actual, Context));
        }

        /// <summary>
        /// Are the given unexpected and actual values unequal?
        /// </summary>
        /// <param name="unexpected">Unexpected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are unequal; otherwise, false.</returns>
        protected bool AreNotEqual<TValue>(TValue unexpected, TValue actual)
        {
            return Log.Add(Assertion.AreNotEqual(unexpected, actual, Context));
        }

        /// <summary>
        /// Is the given condition true?
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <returns>True if the condition is true; otherwise, false.</returns>
        protected bool IsTrue(bool condition)
        {
            return Log.Add(Assertion.IsTrue(condition, Context));
        }

        /// <summary>
        /// Is the given condition false?
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <returns>True if the condition is false; otherwise, false.</returns>
        protected bool IsFalse(bool condition)
        {
            return Log.Add(Assertion.IsFalse(condition, Context));
        }

        /// <summary>
        /// Is the given value equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValue">Reference type.</typeparam>
        /// <returns>True if the value is equal to null; otherwise, false.</returns>
        protected bool IsNull<TValue>(TValue value) where TValue : class
        {
            return Log.Add(Assertion.IsNull(value, Context));
        }

        /// <summary>
        /// Is the given value equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the value is equal to null; otherwise, false.</returns>
        protected bool IsNull<TValue>(TValue? value) where TValue : struct
        {
            return Log.Add(Assertion.IsNull(value, Context));
        }

        /// <summary>
        /// Is the given value not equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValue">Reference type.</typeparam>
        /// <returns>True if the value is not equal to null; otherwise, false.</returns>
        protected bool IsNotNull<TValue>(TValue value) where TValue : class
        {
            return Log.Add(Assertion.IsNotNull(value, Context));
        }

        /// <summary>
        /// Is the given value not equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the value is not equal to null; otherwise, false.</returns>
        protected bool IsNotNull<TValue>(TValue? value) where TValue : struct
        {
            return Log.Add(Assertion.IsNotNull(value, Context));
        }

        /// <summary>
        /// Is the given left hand side value greater than the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is greater than the right hand side value; otherwise, false.</returns>
        protected bool IsGreaterThan<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsGreaterThan(lhs, rhs, Context));
        }

        /// <summary>
        /// Is the given left hand side value greater than or equal to the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is greater than or equal to the right hand side value; otherwise, false.</returns>
        protected bool IsGreaterThanOrEqualTo<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsGreaterThanOrEqualTo(lhs, rhs, Context));
        }

        /// <summary>
        /// Is the given left hand side value less than the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is less than the right hand side value; otherwise, false.</returns>
        protected bool IsLessThan<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsLessThan(lhs, rhs, Context));
        }

        /// <summary>
        /// Is the given left hand side value less than or equal to the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is less than or equal to the right hand side value; otherwise, false.</returns>
        protected bool IsLessThanOrEqualTo<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsLessThanOrEqualTo(lhs, rhs, Context));
        }

        /// <summary>
        /// Does the given collection of elements contain the given element?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="element">Element.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection of elements contains the element; otherwise, false.</returns>
        protected bool Contains<TValue>(IEnumerable<TValue> elements, TValue element)
        {
            return Log.Add(Assertion.Contains(elements, element, Context));
        }

        /// <summary>
        /// Does the given collection of elements not contain the given element?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="element">Element.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection of elements does not contain the element; otherwise, false.</returns>
        protected bool DoesNotContain<TValue>(IEnumerable<TValue> elements, TValue element)
        {
            return Log.Add(Assertion.DoesNotContain(elements, element, Context));
        }

        /// <summary>
        /// Are the sequences of the given collections the same?
        /// </summary>
        /// <param name="expected">Expected sequence.</param>
        /// <param name="actual">Actual sequence.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the sequences are the same; otherwise, false.</returns>
        protected bool SequenceEqual<TValue>(IEnumerable<TValue> expected, IEnumerable<TValue> actual)
        {
            return Log.Add(Assertion.SequenceEqual(expected, actual, Context));
        }

        /// <summary>
        /// Is the given collection empty?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection is empty; otherwise, false.</returns>
        protected bool IsEmpty<TValue>(IEnumerable<TValue> elements)
        {
            return Log.Add(Assertion.IsEmpty(elements, Context));
        }

        /// <summary>
        /// Is the given collection not empty?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection is not empty; otherwise, false.</returns>
        protected bool IsNotEmpty<TValue>(IEnumerable<TValue> elements)
        {
            return Log.Add(Assertion.IsNotEmpty(elements, Context));
        }

        /// <summary>
        /// Is the given expected type assignable to the given actual type?
        /// </summary>
        /// <param name="expected">Expected type.</param>
        /// <param name="actual">Actual type.</param>
        /// <returns>True if the expected type is assignable to the actual type; otherwise, false.</returns>
        protected bool IsAssignableTo(Type expected, Type actual)
        {
            return Log.Add(Assertion.IsAssignableTo(expected, actual, Context));
        }

        /// <summary>
        /// Is the given value's type assignable to the given type?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="type">Type.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the value's type is assignable to the type; otherwise, false.</returns>
        protected bool IsInstanceOfType<TValue>(TValue value, Type type)
        {
            return Log.Add(Assertion.IsInstanceOfType(value, type, Context));
        }

        /// <summary>
        /// Gets the value associated with the specified key from the given dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary.</param>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the dictionary contains a value with the specified key; otherwise, false.</returns>
        protected bool TryGetValue<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            return Log.Add(Assertion.IsTrue(dictionary.TryGetValue(key, out value), Context));
        }

        /// <summary>
        /// Handles the exception thrown by the given action.
        /// </summary>
        /// <param name="action">Action that should throw an exception.</param>
        /// <typeparam name="TException">Exception type.</typeparam>
        /// <returns>True if the expected exception was caught; otherwise, false.</returns>
        protected bool Throws<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                return Log.Add(Assertion.AreEqual(typeof(TException), null, Context));
            }
            catch (Exception exception)
            {
                return Log.Add(Assertion.AreEqual(typeof(TException), exception.GetType(), Context));
            }
        }

        /// <summary>
        /// Retries the given action until there are no logged failures or the maximum number of attempts has been reached.
        /// </summary>
        /// <param name="action">Action to retry.</param>
        /// <param name="attempts">The maximum number of attempts.</param>
        /// <exception cref="ArgumentException">The number of attempts is less than two.</exception>
        protected void Retry(Action action, int attempts)
        {
            if (attempts <= 1)
            {
                throw new ArgumentException("The number of attempts must be at least two.", nameof(attempts));
            }

            var log = Log;

            for (var attempt = 0; attempt < attempts; attempt++)
            {
                Log = new Log(log.OnlyFailures);
                action();

                if (Log) break;
            }

            foreach (var assertion in Log)
            {
                log.Add(assertion);
            }

            Log = log;
        }
    }
}