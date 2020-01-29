using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class ConditionTest : Test
    {
        [TestMethod]
        public void True()
        {
            IsTrue(AreEqual(1, 1));
            IsTrue(AreNotEqual(1, 2));
            IsTrue(IsTrue(true));
            IsTrue(IsFalse(false));
            IsTrue(IsNull<List<int>>(null));
            IsTrue(IsNull<int>(null));
            IsTrue(IsNotNull(new List<int>()));
            IsTrue(IsNotNull<int>(1));
            IsTrue(IsGreaterThan(2, 1));
            IsTrue(IsGreaterThanOrEqualTo(2, 1));
            IsTrue(IsLessThan(1, 2));
            IsTrue(IsLessThanOrEqualTo(1, 2));
            IsTrue(Contains("test", 's'));
            IsTrue(DoesNotContain("test", 'x'));
            IsTrue(SequenceEqual("test", "test"));
            IsTrue(IsEmpty(""));
            IsTrue(IsNotEmpty("test"));
            IsTrue(Throws<ArgumentOutOfRangeException>(() => new List<int>()[1] = 2));
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void False()
        {
            var passed = IsFalse(AreNotEqual(1, 1)) &&
                         IsFalse(AreEqual(1, 2)) &&
                         IsFalse(IsFalse(true)) &&
                         IsFalse(IsTrue(false)) &&
                         IsFalse(IsNotNull<List<int>>(null)) &&
                         IsFalse(IsNotNull<int>(null)) &&
                         IsFalse(IsNull(new List<int>())) &&
                         IsFalse(IsNull<int>(1)) &&
                         IsFalse(IsLessThan(2, 1)) &&
                         IsFalse(IsLessThanOrEqualTo(2, 1)) &&
                         IsFalse(IsGreaterThan(1, 2)) &&
                         IsFalse(IsGreaterThanOrEqualTo(1, 2)) &&
                         IsFalse(DoesNotContain("test", 's')) &&
                         IsFalse(Contains("test", 'x')) &&
                         IsFalse(SequenceEqual("test", "tes")) &&
                         IsFalse(IsNotEmpty("")) &&
                         IsFalse(IsEmpty("test")) &&
                         IsFalse(Throws<NullReferenceException>(() => new List<int>()[1] = 2));

            if (passed)
            {
                Cleanup();
            }
        }
    }
}