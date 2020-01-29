using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace Tricorder.NET
{
    public abstract class Test
    {
        protected Test(bool onlyFailures = false)
        {
            Log = new Log(onlyFailures);
            Context = new StackTraceContext(GetType());
        }

        private Log Log { get; }

        private StackTraceContext Context { get; }

        private bool Cleaned { get; set; }

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

        protected bool AreEqual<TValue>(TValue expected, TValue actual)
        {
            return Log.Add(Assertion.AreEqual(expected, actual, Context));
        }

        protected bool AreNotEqual<TValue>(TValue unexpected, TValue actual)
        {
            return Log.Add(Assertion.AreNotEqual(unexpected, actual, Context));
        }

        protected bool IsTrue(bool condition)
        {
            return Log.Add(Assertion.IsTrue(condition, Context));
        }

        protected bool IsFalse(bool condition)
        {
            return Log.Add(Assertion.IsFalse(condition, Context));
        }

        protected bool IsNull<TValue>(TValue value) where TValue : class
        {
            return Log.Add(Assertion.IsNull(value, Context));
        }

        protected bool IsNull<TValue>(TValue? value) where TValue : struct
        {
            return Log.Add(Assertion.IsNull(value, Context));
        }

        protected bool IsNotNull<TValue>(TValue value) where TValue : class
        {
            return Log.Add(Assertion.IsNotNull(value, Context));
        }

        protected bool IsNotNull<TValue>(TValue? value) where TValue : struct
        {
            return Log.Add(Assertion.IsNotNull(value, Context));
        }

        protected bool IsGreaterThan<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsGreaterThan(lhs, rhs, Context));
        }

        protected bool IsGreaterThanOrEqualTo<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsGreaterThanOrEqualTo(lhs, rhs, Context));
        }

        protected bool IsLessThan<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsLessThan(lhs, rhs, Context));
        }

        protected bool IsLessThanOrEqualTo<TValue>(TValue lhs, TValue rhs) where TValue : IComparable
        {
            return Log.Add(Assertion.IsLessThanOrEqualTo(lhs, rhs, Context));
        }

        protected bool Contains<TValue>(IEnumerable<TValue> elements, TValue element)
        {
            return Log.Add(Assertion.Contains(elements, element, Context));
        }

        protected bool DoesNotContain<TValue>(IEnumerable<TValue> elements, TValue element)
        {
            return Log.Add(Assertion.DoesNotContain(elements, element, Context));
        }

        protected bool SequenceEqual<TValue>(IEnumerable<TValue> expected, IEnumerable<TValue> actual)
        {
            return Log.Add(Assertion.SequenceEqual(expected, actual, Context));
        }

        protected bool IsEmpty<TValue>(IEnumerable<TValue> elements)
        {
            return Log.Add(Assertion.IsEmpty(elements, Context));
        }

        protected bool IsNotEmpty<TValue>(IEnumerable<TValue> elements)
        {
            return Log.Add(Assertion.IsNotEmpty(elements, Context));
        }

        protected bool TryGetValue<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            return Log.Add(Assertion.IsTrue(dictionary.TryGetValue(key, out value), Context));
        }

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
    }
}