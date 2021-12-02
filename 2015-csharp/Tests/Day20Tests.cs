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
            Assert.AreEqual(1, Program.SolvePartA(10));
            Assert.AreEqual(2, Program.SolvePartA(30));
            Assert.AreEqual(3, Program.SolvePartA(40));
            Assert.AreEqual(4, Program.SolvePartA(70));
            Assert.AreEqual(4, Program.SolvePartA(60));
            Assert.AreEqual(6, Program.SolvePartA(120));
            Assert.AreEqual(6, Program.SolvePartA(80));
            Assert.AreEqual(8, Program.SolvePartA(150));
            Assert.AreEqual(8, Program.SolvePartA(130));
        }
    }
}