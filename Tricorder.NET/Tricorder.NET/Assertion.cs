using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Collections.Comparer;

namespace Tricorder.NET
{
    internal sealed class Assertion
    {
        private Assertion(bool state, string name, string message, StackTraceContext context)
        {
            State = state;
            Name = name;
            Message = message;
            StackFrame = state ? null : context.GetFrames().First();
        }

        private bool State { get; }

        private string Name { get; }

        private string Message { get; }

        private StackFrame StackFrame { get; }

        public static implicit operator bool(Assertion assertion)
        {
            return assertion.State;
        }

        internal static Assertion AreEqual<TValue>(TValue expected, TValue actual, StackTraceContext context)
        {
            return AreEqual(expected, actual, nameof(AreEqual), context);
        }

        internal static Assertion AreNotEqual<TValue>(TValue unexpected, TValue actual, StackTraceContext context)
        {
            return AreNotEqual(unexpected, actual, nameof(AreNotEqual), context);
        }

        internal static Assertion IsTrue(bool condition, StackTraceContext context)
        {
            return AreEqual(true, condition, nameof(IsTrue), context);
        }

        internal static Assertion IsFalse(bool condition, StackTraceContext context)
        {
            return AreEqual(false, condition, nameof(IsFalse), context);
        }

        internal static Assertion IsNull<TValue>(TValue value, StackTraceContext context) where TValue : class
        {
            return AreEqual(null, value, nameof(IsNull), context);
        }

        internal static Assertion IsNull<TValue>(TValue? value, StackTraceContext context) where TValue : struct
        {
            return AreEqual(null, value, nameof(IsNull), context);
        }

        internal static Assertion IsNotNull<TValue>(TValue value, StackTraceContext context) where TValue : class
        {
            return AreNotEqual(null, value, nameof(IsNotNull), context);
        }

        internal static Assertion IsNotNull<TValue>(TValue? value, StackTraceContext context) where TValue : struct
        {
            return AreNotEqual(null, value, nameof(IsNotNull), context);
        }

        internal static Assertion IsGreaterThan<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) > 0
                ? new Assertion(true, nameof(IsGreaterThan), $"{ToString(lhs)} is greater than {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsGreaterThan), $"{ToString(lhs)} is not greater than {ToString(rhs)}", context);
        }

        internal static Assertion IsGreaterThanOrEqualTo<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) >= 0
                ? new Assertion(true, nameof(IsGreaterThanOrEqualTo), $"{ToString(lhs)} is greater than or equal to {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsGreaterThanOrEqualTo), $"{ToString(lhs)} is not greater than nor equal to {ToString(rhs)}", context);
        }

        internal static Assertion IsLessThan<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) < 0
                ? new Assertion(true, nameof(IsLessThan), $"{ToString(lhs)} is less than {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsLessThan), $"{ToString(lhs)} is not less than {ToString(rhs)}", context);
        }

        internal static Assertion IsLessThanOrEqualTo<TValue>(TValue lhs, TValue rhs, StackTraceContext context) where TValue : IComparable
        {
            return Default.Compare(lhs, rhs) <= 0
                ? new Assertion(true, nameof(IsLessThanOrEqualTo), $"{ToString(lhs)} is less than or equal to {ToString(rhs)}", context)
                : new Assertion(false, nameof(IsLessThanOrEqualTo), $"{ToString(lhs)} is not less than nor equal to {ToString(rhs)}", context);
        }

        internal static Assertion Contains<TValue>(IEnumerable<TValue> elements, TValue element, StackTraceContext context)
        {
            return elements.Contains(element)
                ? new Assertion(true, nameof(Contains), $"{ToString(element)} was found.", context)
                : new Assertion(false, nameof(Contains), $"{ToString(element)} was not found.", context);
        }

        internal static Assertion DoesNotContain<TValue>(IEnumerable<TValue> elements, TValue element, StackTraceContext context)
        {
            return !elements.Contains(element)
                ? new Assertion(true, nameof(DoesNotContain), $"{ToString(element)} was not found.", context)
                : new Assertion(false, nameof(DoesNotContain), $"{ToString(element)} was found.", context);
        }

        internal static Assertion SequenceEqual<TValue>(IEnumerable<TValue> expected, IEnumerable<TValue> actual, StackTraceContext context)
        {
            return expected.SequenceEqual(actual)
                ? new Assertion(true, nameof(SequenceEqual), "The two sequences are equal.", context)
                : new Assertion(false, nameof(SequenceEqual), "The two sequences are not equal.", context);
        }

        internal static Assertion IsEmpty<TValue>(IEnumerable<TValue> elements, StackTraceContext context)
        {
            return elements.Any()
                ? new Assertion(false, nameof(IsEmpty), "Collection is not empty.", context)
                : new Assertion(true, nameof(IsEmpty), "Collection is empty.", context);
        }

        internal static Assertion IsNotEmpty<TValue>(IEnumerable<TValue> elements, StackTraceContext context)
        {
            return elements.Any()
                ? new Assertion(true, nameof(IsNotEmpty), "Collection is not empty.", context)
                : new Assertion(false, nameof(IsNotEmpty), "Collection is empty.", context);
        }

        private static Assertion AreEqual<TValue>(TValue expected, TValue actual, string name, StackTraceContext context)
        {
            return Equals(expected, actual)
                ? new Assertion(true, name, $"Expected {ToString(expected)} and got {ToString(actual)}.", context)
                : new Assertion(false, name, $"Expected {ToString(expected)} but got {ToString(actual)}.", context);
        }

        private static Assertion AreNotEqual<TValue>(TValue unexpected, TValue actual, string name, StackTraceContext context)
        {
            return !Equals(unexpected, actual)
                ? new Assertion(true, name, $"Did not expect {ToString(unexpected)} and got {ToString(actual)}.", context)
                : new Assertion(false, name, $"Did not expect {ToString(unexpected)} but got {ToString(actual)}.", context);
        }

        private static string ToString<TValue>(TValue value)
        {
            return value == null ? "null" : value.ToString();
        }

        public override string ToString()
        {
            return State ? $" + {Name} passed. {Message}" : $" - {Name} failed: {Message} {StackFrame.GetFileName()}: line {StackFrame.GetFileLineNumber()}";
        }
    }
}