using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tricorder.NET;

namespace Tricorder.NET.Tests
{
    [TestClass]
    public class StackTraceTest : Test
    {
        [TestMethod]
        public void LineNumber()
        {
            var context = new StackTraceContext(GetType());
            var (expected, actual) = LineNumbers.Get(context);

            AreEqual(expected, actual);
        }

        [TestMethod]
        public void Delegate()
        {
            var context = new StackTraceContext(GetType());
            var (expected1, actual1) = Delegate1();
            var (expected2, actual2) = Delegate2(context);

            AreEqual(expected1, actual1);
            AreEqual(expected2, actual2);

            (int Expected, int Actaul) Delegate1()
            {
                return LineNumbers.Get(context);
            }

            static (int Expected, int Actual) Delegate2(StackTraceContext context)
            {
                return LineNumbers.Get(context);
            }
        }
    }

    internal static class LineNumbers
    {
        public static (int Expected, int Actual) Get(StackTraceContext context, [CallerLineNumber] int lineNumber = 0)
        {
            var stackFrame = context.First();

            return (lineNumber, stackFrame.GetFileLineNumber());
        }
    }
}