using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.Tests
{
    [TestClass]
    public class ComparisonTest : Test
    {
        [TestMethod]
        public void IsGreaterThan()
        {
            IsGreaterThan(1, 0);
            IsGreaterThan(1L, 0L);
            IsGreaterThan(1.0f, 0.0f);
            IsGreaterThan(1.0, 0.0);
            IsGreaterThan(1.0M, 0.0M);
            IsGreaterThan("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsGreaterThanException()
        {
            IsGreaterThan(0, 1);
            Cleanup();
        }

        [TestMethod]
        public void IsGreaterThanOrEqualTo()
        {
            IsGreaterThanOrEqualTo(1, 0);
            IsGreaterThanOrEqualTo(1L, 0L);
            IsGreaterThanOrEqualTo(1.0f, 0.0f);
            IsGreaterThanOrEqualTo(1.0, 0.0);
            IsGreaterThanOrEqualTo(1.0M, 0.0M);
            IsGreaterThanOrEqualTo("test", null);

            IsGreaterThanOrEqualTo(1, 1);
            IsGreaterThanOrEqualTo(1L, 1L);
            IsGreaterThanOrEqualTo(1.0f, 1.0f);
            IsGreaterThanOrEqualTo(1.0, 1.0);
            IsGreaterThanOrEqualTo(1.0M, 1.0M);
            IsGreaterThanOrEqualTo("test", "test");
        }
        
        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsGreaterThanOrEqualToException()
        {
            IsGreaterThanOrEqualTo(0, 1);
            Cleanup();
        }

        [TestMethod]
        public void IsLessThan()
        {
            IsLessThan(0, 1);
            IsLessThan(0L, 1L);
            IsLessThan(0.0f, 1.0f);
            IsLessThan(0.0, 1.0);
            IsLessThan(0.0M, 1.0M);
            IsLessThan(null, "test");
        }
        
        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsLessThanException()
        {
            IsLessThan(1, 0);
            Cleanup();
        }

        [TestMethod]
        public void IsLessThanOrEqualTo()
        {
            IsLessThanOrEqualTo(0, 1);
            IsLessThanOrEqualTo(0L, 1L);
            IsLessThanOrEqualTo(0.0f, 1.0f);
            IsLessThanOrEqualTo(0.0, 1.0);
            IsLessThanOrEqualTo(0.0M, 1.0M);
            IsLessThanOrEqualTo(null, "test");

            IsLessThanOrEqualTo(1, 1);
            IsLessThanOrEqualTo(1L, 1L);
            IsLessThanOrEqualTo(1.0f, 1.0f);
            IsLessThanOrEqualTo(1.0, 1.0);
            IsLessThanOrEqualTo(1.0M, 1.0M);
            IsLessThanOrEqualTo("test", "test");
        }
        
        [TestMethod]
        [ExpectedException(typeof(TestFailedException))]
        public void IsLessThanOrEqualToException()
        {
            IsLessThanOrEqualTo(1, 0);
            Cleanup();
        }
    }
}