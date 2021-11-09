using Day20;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day20Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(10, Program.SolvePartA(1));
            Assert.AreEqual(30, Program.SolvePartA(2));
            Assert.AreEqual(40, Program.SolvePartA(3));
            Assert.AreEqual(70, Program.SolvePartA(4));
            Assert.AreEqual(60, Program.SolvePartA(5));
            Assert.AreEqual(120, Program.SolvePartA(6));
            Assert.AreEqual(80, Program.SolvePartA(7));
            Assert.AreEqual(150, Program.SolvePartA(8));
            Assert.AreEqual(130, Program.SolvePartA(9));
        }
    }
}