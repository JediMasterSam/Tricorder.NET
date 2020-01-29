using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.Tests
{
    [TestClass]
    public class LogTest : Test
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(1, 2)]
        public void Add(int a, int b)
        {
            var log = new Log(false);
            var context = new StackTraceContext(GetType());

            log.Add(Assertion.AreEqual(a, a, context));
            log.Add(Assertion.AreEqual(a, a, context));
            log.Add(Assertion.AreEqual(a, b, context));

            AreEqual(3, log.Count);

            if (a == b)
            {
                IsTrue(log);
            }
            else
            {
                AreEqual(1, log.Failures);
                IsFalse(log);
            }
        }

        [TestMethod]
        public void OnlyFailures()
        {
            var log = new Log(false);
            var context = new StackTraceContext(GetType());

            log.Add(Assertion.AreEqual(1, 1, context));
            log.Add(Assertion.AreEqual(1, 1, context));
            log.Add(Assertion.AreEqual(1, 2, context));
            log.Add(Assertion.AreEqual(1, 1, context));
            log.Add(Assertion.AreEqual(1, 2, context));

            AreEqual(5, log.Count);
            AreEqual(2, log.Failures);
            IsFalse(log);

            foreach (var assertion in log)
            {
                IsFalse(assertion);
            }
        }
    }
}