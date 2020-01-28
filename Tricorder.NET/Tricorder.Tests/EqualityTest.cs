using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.Tests
{
    [TestClass]
    public class EqualityTest : Test
    {
        [TestMethod]
        public void AreEqual()
        {
            var list = new List<int>();

            unchecked
            {
                AreEqual((sbyte) -102, (sbyte) 0x9A);
            }

            AreEqual((short) 1034, (short) 0x040A);
            AreEqual(90946, 0x16342);
            AreEqual(4294967296, 0x100000000);
            AreEqual((byte) 201, (byte) 0x00C9);
            AreEqual((ushort) 65034, (ushort) 0xFE0A);
            AreEqual(3000000000, 0xB2D05E00);
            AreEqual((ulong) 7934076125, (ulong) 0x0001D8e864DD);
            AreEqual(1.234f, 1.0f + 0.234f);
            AreEqual(1.234, 1.0 + 0.234);
            AreEqual('X', '\x0058');
            AreEqual(0.999M, 0.499M + 0.50M);
            AreEqual("test", "te" + "st");
            AreEqual(typeof(int), 1.GetType());
            AreEqual(list, list);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void AreEqualException()
        {
            AreEqual(1, 2);
            Cleanup();
        }

        [TestMethod]
        public void AreNotEqual()
        {
            var list = new List<int>();

            unchecked
            {
                AreNotEqual((sbyte) -101, (sbyte) 0x9A);
            }

            AreNotEqual((short) 1031, (short) 0x040A);
            AreNotEqual(90941, 0x16342);
            AreNotEqual(4294967291, 0x100000000);
            AreNotEqual((byte) 200, (byte) 0x00C9);
            AreNotEqual((ushort) 65031, (ushort) 0xFE0A);
            AreNotEqual(3000000001, 0xB2D05E00);
            AreNotEqual((ulong) 7934076121, (ulong) 0x0001D8e864DD);
            AreNotEqual(1.231f, 1.0f + 0.234f);
            AreNotEqual(1.231, 1.0 + 0.234);
            AreNotEqual('Y', '\x0058');
            AreNotEqual(0.991M, 0.499M + 0.50M);
            AreNotEqual("tests", "te" + "st");
            AreNotEqual(typeof(long), 1.GetType());
            AreNotEqual(list, new List<int>());
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void AreNotEqualException()
        {
            AreNotEqual(1, 1);
            Cleanup();
        }

        [TestMethod]
        public void IsTrue()
        {
            IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsTrueException()
        {
            IsTrue(false);
            Cleanup();
        }

        [TestMethod]
        public void IsFalse()
        {
            IsFalse(false);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsFalseException()
        {
            IsFalse(true);
            Cleanup();
        }

        [TestMethod]
        public void IsNull()
        {
            IsNull<List<int>>(null);
            IsNull<int>(null);
        }


        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsNullException()
        {
            IsNull(new List<int>());
            Cleanup();
        }

        [TestMethod]
        public void IsNotNull()
        {
            IsNotNull(new List<int>());
            IsNotNull((int?) 1);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsNotNullException()
        {
            IsNotNull<List<int>>(null);
            Cleanup();
        }
    }
}