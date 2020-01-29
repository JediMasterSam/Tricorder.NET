using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class ContainsTest : Test
    {
        [TestMethod]
        public void Contains()
        {
            Contains(new[] {1, 2, 3}, 3);
            Contains("test", 's');
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void ContainsException()
        {
            Contains("test", 'x');
            Cleanup();
        }

        [TestMethod]
        public void DoesNotContain()
        {
            DoesNotContain(new[] {1, 2, 3}, 4);
            DoesNotContain("test", 'x');
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void DoesNotContainException()
        {
            DoesNotContain("test", 's');
            Cleanup();
        }

        [TestMethod]
        public void SequenceEqual()
        {
            SequenceEqual(new[] {1, 2, 3}, new[] {1, 2, 3});
            SequenceEqual("test", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void SequenceEqualException()
        {
            SequenceEqual("test", "tes");
            Cleanup();
        }

        [TestMethod]
        public void IsEmpty()
        {
            IsEmpty(new int[0]);
            IsEmpty("");
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsEmptyException()
        {
            IsEmpty("test");
            Cleanup();
        }

        [TestMethod]
        public void IsNotEmpty()
        {
            IsNotEmpty(new[] {1, 2, 3});
            IsNotEmpty("test");
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsNotEmptyException()
        {
            IsNotEmpty("");
            Cleanup();
        }

        [TestMethod]
        public void TryGetValue()
        {
            var dictionary = new Dictionary<int, int> {{1, 2}};

            if (TryGetValue(dictionary, 1, out var value))
            {
                AreEqual(2, value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void TryGetValueException()
        {
            var dictionary = new Dictionary<int, int> {{1, 2}};

            if (TryGetValue(dictionary, 3, out var value))
            {
                AreEqual(2, value);
            }

            Cleanup();
        }
    }
}