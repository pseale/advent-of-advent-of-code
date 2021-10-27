using Day03;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day03Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(2, Program.SolvePartA(">"));
            Assert.AreEqual(4, Program.SolvePartA("^>v<"));
            Assert.AreEqual(2, Program.SolvePartA("^v^v^v^v^v"));
        }

        [Test]
        public void PartB()
        {
            Assert.AreEqual(3, Program.SolvePartB("^v"));
            Assert.AreEqual(3, Program.SolvePartB("^>v<"));
            Assert.AreEqual(11, Program.SolvePartB("^v^v^v^v^v"));
        }
    }
}