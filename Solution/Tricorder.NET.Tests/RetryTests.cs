using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class RetryTests : Test
    {
        [TestMethod]
        public void Retry()
        {
            const int expected = 3;
            var actual = 0;

            Retry(() => { AreEqual(expected, ++actual); }, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void RetryException1()
        {
            const int expected = 3;
            var actual = 0;

            Retry(() => { AreEqual(expected, ++actual); }, expected - 1);
            Cleanup();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RetryException2()
        {
            Retry(() => { AreEqual(0, 0); }, 0);
        }
    }
}