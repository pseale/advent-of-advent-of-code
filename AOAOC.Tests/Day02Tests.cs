using Day02;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day02Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(58, Program.SolvePartA("2x3x4"));

            Assert.AreEqual(43, Program.SolvePartA("1x1x10"));

            // test multiple lines, plus blank lines
            Assert.AreEqual(43 + 58, Program.SolvePartA("1x1x10\r\n2x3x4\r\n"));
        }
    }
}