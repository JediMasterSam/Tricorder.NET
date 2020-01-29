using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class ExceptionTest : Test
    {
        [TestMethod]
        public void Throws()
        {
            Throws<ArgumentOutOfRangeException>(() => new List<int>()[1] = 2);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void ThrowsException1()
        {
            Throws<ArgumentOutOfRangeException>(() => new List<int>(new[] {1, 2, 3})[1] = 2);
            Cleanup();
        }
        
        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void ThrowsException2()
        {
            Throws<NullReferenceException>(() => new List<int>()[1] = 2);
            Cleanup();
        }
    }
}