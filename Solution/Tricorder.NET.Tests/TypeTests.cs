using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class TypeTests : Test
    {
        [TestMethod]
        public void IsAssignableTo()
        {
            IsAssignableTo(typeof(int[]), typeof(int[]));
            IsAssignableTo(typeof(int[]), typeof(IEnumerable<int>));
        }

        [TestMethod]
        [DataRow(typeof(int[]), null)]
        [DataRow(typeof(int[]), typeof(int))]
        [DataRow(null, typeof(int[]))]
        [ExpectedException(typeof(TestFailedException))]
        public void IsAssignableToException(Type expected, Type actual)
        {
            IsAssignableTo(expected, actual);
            Cleanup();
        }

        [TestMethod]
        public void IsInstanceOfType()
        {
            var array = new int[0];

            IsInstanceOfType(array, typeof(int[]));
            IsInstanceOfType(array, typeof(IEnumerable<int>));
        }

        [TestMethod]
        [DataRow(null, typeof(int[]))]
        [DataRow(new int[0], null)]
        [DataRow(new int[0], typeof(int))]
        [ExpectedException(typeof(TestFailedException))]
        public void IsInstanceOfTypeException(int[] array, Type type)
        {
            IsInstanceOfType(array, type);
            Cleanup();
        }
    }
}