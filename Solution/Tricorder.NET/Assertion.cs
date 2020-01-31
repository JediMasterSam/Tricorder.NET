using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Collections.Comparer;

namespace Tricorder.NET
{
    /// <summary>
    /// Represents a statement of fact.
    /// </summary>
    internal sealed class Assertion
    {
        /// <summary>
        /// Creates a new instance of <see cref="Assertion"/>.
        /// </summary>
        /// <param name="state">Is this fact true or false?</param>
        /// <param name="name">The name of the function that generated the state.</param>
        /// <param name="message">Explains the state's value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        private Assertion(bool state, string name, string message, StackTraceContext context)
        {
            State = state;
            Name = name;
            Message = message;
            StackFrame = state ? null : context.First();
        }

        /// <summary>
        /// Is the fact true or false?
        /// </summary>
        private bool State { get; }

        /// <summary>
        /// The name of the function that generated the current state.
        /// </summary>
        private string Name { get; }

        /// <summary>
        /// Explains the current state's value.
        /// </summary>
        private string Message { get; }

        /// <summary>
        /// The <see cref="StackFrame"/> that issued the current state.
        /// </summary>
        private StackFrame StackFrame { get; }

        /// <summary>
        /// Implicitly converts the given <see cref="Assertion"/> to its state.
        /// </summary>
        /// <param name="assertion">Assertion of which to get the state.</param>
        /// <returns>True if the state is true; otherwise, false.</returns>
        public static implicit operator bool(Assertion assertion)
        {
            return assertion.State;
        }

        /// <summary>
        /// Are the given expected and actual values equal?
        /// </summary>
        /// <param name="expected">Expected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are equal; otherwise, false.</returns>
        internal static Assertion AreEqual<TValue>(TValue expected, TValue actual, StackTraceContext context)
        {
            return AreEqual(expected, actual, nameof(AreEqual), context);
        }

        /// <summary>
        /// Are the given unexpected and actual values unequal?
        /// </summary>
        /// <param name="unexpected">Unexpected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are unequal; otherwise, false.</returns>
        internal static Assertion AreNotEqual<TValue>(TValue unexpected, TValue actual, StackTraceContext context)
        {
            return AreNotEqual(unexpected, actual, nameof(AreNotEqual), context);
        }

        /// <summary>
        /// Is the given condition true?
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <returns>True if the condition is true; otherwise, false.</returns>
        internal static Assertion IsTrue(bool condition, StackTraceContext context)
        {
            return AreEqual(true, condition, nameof(IsTrue), context);
        }

        /// <summary>
        /// Is the given condition false?
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <returns>True if the condition is false; otherwise, false.</returns>
        internal static Assertion IsFalse(bool condition, StackTraceContext context)
        {
            return AreEqual(false, condition, nameof(IsFalse), context);
        }

        /// <summary>
        /// Is the given value equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Reference type.</typeparam>
        /// <returns>True if the value is equal to null; otherwise, false.</returns>
        internal static Assertion IsNull<TValue>(TValue value, StackTraceContext context) where TValue : class
        {
            return AreEqual(null, value, nameof(IsNull), context);
        }

        /// <summary>
        /// Is the given value equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the value is equal to null; otherwise, false.</returns>
        internal static Assertion IsNull<TValue>(TValue? value, StackTraceContext context) where TValue : struct
        {
            return AreEqual(null, value, nameof(IsNull), context);
        }

        /// <summary>
        /// Is the given value not equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Reference type.</typeparam>
        /// <returns>True if the value is not equal to null; otherwise, false.</returns>
        internal static Assertion IsNotNull<TValue>(TValue value, StackTraceContext context) where TValue : class
        {
            return AreNotEqual(null, value, nameof(IsNotNull), context);
        }

        /// <summary>
        /// Is the given value not equal to null?
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the value is not equal to null; otherwise, false.</returns>
        internal static Assertion IsNotNull<TValue>(TValue? value, StackTraceContext context) where TValue : struct
        {
            return AreNotEqual(null, value, nameof(IsNotNull), context);
        }

        /// <summary>
        /// Is the given left hand side value greater than the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is greater than the right hand side value; otherwise, false.</returns>
        internal static Assertion IsGreaterThan<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) > 0
                ? new Assertion(true, nameof(IsGreaterThan), $"{ToString(lhs)} is greater than {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsGreaterThan), $"{ToString(lhs)} is not greater than {ToString(rhs)}", context);
        }

        /// <summary>
        /// Is the given left hand side value greater than or equal to the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is greater than or equal to the right hand side value; otherwise, false.</returns>
        internal static Assertion IsGreaterThanOrEqualTo<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) >= 0
                ? new Assertion(true, nameof(IsGreaterThanOrEqualTo), $"{ToString(lhs)} is greater than or equal to {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsGreaterThanOrEqualTo), $"{ToString(lhs)} is not greater than nor equal to {ToString(rhs)}", context);
        }

        /// <summary>
        /// Is the given left hand side value less than the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is less than the right hand side value; otherwise, false.</returns>
        internal static Assertion IsLessThan<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) < 0
                ? new Assertion(true, nameof(IsLessThan), $"{ToString(lhs)} is less than {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsLessThan), $"{ToString(lhs)} is not less than {ToString(rhs)}", context);
        }

        /// <summary>
        /// Is the given left hand side value less than or equal to the given right hand side value?
        /// </summary>
        /// <param name="lhs">Left hand side.</param>
        /// <param name="rhs">Right hand side.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Comparable type.</typeparam>
        /// <returns>True if the left hand side value is less than or equal to the right hand side value; otherwise, false.</returns>
        internal static Assertion IsLessThanOrEqualTo<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) <= 0
                ? new Assertion(true, nameof(IsLessThanOrEqualTo), $"{ToString(lhs)} is less than or equal to {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsLessThanOrEqualTo), $"{ToString(lhs)} is not less than nor equal to {ToString(rhs)}", context);
        }

        /// <summary>
        /// Does the given collection of elements contain the given element?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="element">Element.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection of elements contains the element; otherwise, false.</returns>
        internal static Assertion Contains<TValue>(IEnumerable<TValue> elements, TValue element, StackTraceContext context)
        {
            return elements.Contains(element)
                ? new Assertion(true, nameof(Contains), $"{ToString(element)} was found.", context)
                : new Assertion(false, nameof(Contains), $"{ToString(element)} was not found.", context);
        }

        /// <summary>
        /// Does the given collection of elements not contain the given element?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="element">Element.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection of elements does not contain the element; otherwise, false.</returns>
        internal static Assertion DoesNotContain<TValue>(IEnumerable<TValue> elements, TValue element, StackTraceContext context)
        {
            return !elements.Contains(element)
                ? new Assertion(true, nameof(DoesNotContain), $"{ToString(element)} was not found.", context)
                : new Assertion(false, nameof(DoesNotContain), $"{ToString(element)} was found.", context);
        }

        /// <summary>
        /// Are the sequences of the given collections the same?
        /// </summary>
        /// <param name="expected">Expected sequence.</param>
        /// <param name="actual">Actual sequence.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the sequences are the same; otherwise, false.</returns>
        internal static Assertion SequenceEqual<TValue>(IEnumerable<TValue> expected, IEnumerable<TValue> actual, StackTraceContext context)
        {
            return expected.SequenceEqual(actual)
                ? new Assertion(true, nameof(SequenceEqual), "The two sequences are equal.", context)
                : new Assertion(false, nameof(SequenceEqual), "The two sequences are not equal.", context);
        }

        /// <summary>
        /// Is the given collection empty?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection is empty; otherwise, false.</returns>
        internal static Assertion IsEmpty<TValue>(IEnumerable<TValue> elements, StackTraceContext context)
        {
            return elements.Any()
                ? new Assertion(false, nameof(IsEmpty), "Collection is not empty.", context)
                : new Assertion(true, nameof(IsEmpty), "Collection is empty.", context);
        }

        /// <summary>
        /// Is the given collection not empty?
        /// </summary>
        /// <param name="elements">Elements.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if the collection is not empty; otherwise, false.</returns>
        internal static Assertion IsNotEmpty<TValue>(IEnumerable<TValue> elements, StackTraceContext context)
        {
            return elements.Any()
                ? new Assertion(true, nameof(IsNotEmpty), "Collection is not empty.", context)
                : new Assertion(false, nameof(IsNotEmpty), "Collection is empty.", context);
        }

        /// <summary>
        /// Are the given expected and actual values equal?
        /// </summary>
        /// <param name="expected">Expected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <param name="name">Function name.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are equal; otherwise, false.</returns>
        private static Assertion AreEqual<TValue>(TValue expected, TValue actual, string name, StackTraceContext context)
        {
            return Equals(expected, actual)
                ? new Assertion(true, name, $"Expected {ToString(expected)} and got {ToString(actual)}.", context)
                : new Assertion(false, name, $"Expected {ToString(expected)} but got {ToString(actual)}.", context);
        }


        /// <summary>
        /// Are the given unexpected and actual values unequal?
        /// </summary>
        /// <param name="unexpected">Unexpected value.</param>
        /// <param name="actual">Actual value.</param>
        /// <param name="name">Function name.</param>
        /// <param name="context">Gets the originator of that issued the state.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>True if expected and actual are unequal; otherwise, false.</returns>
        private static Assertion AreNotEqual<TValue>(TValue unexpected, TValue actual, string name, StackTraceContext context)
        {
            return !Equals(unexpected, actual)
                ? new Assertion(true, name, $"Did not expect {ToString(unexpected)} and got {ToString(actual)}.", context)
                : new Assertion(false, name, $"Did not expect {ToString(unexpected)} but got {ToString(actual)}.", context);
        }

        /// <summary>
        /// Converts the given value to a string.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <returns>The string representation of the value.</returns>
        private static string ToString<TValue>(TValue value)
        {
            return value == null ? "null" : value.ToString();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return State ? $" + {Name} passed. {Message}" : $" - {Name} failed: {Message} {StackFrame.GetFileName()}: line {StackFrame.GetFileLineNumber()}";
        }
    }
}