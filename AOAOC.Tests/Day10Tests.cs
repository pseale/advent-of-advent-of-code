using Day10;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day10Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual("11", Program.SolvePartA("1", 1));
            Assert.AreEqual("21", Program.SolvePartA("11", 1));
            Assert.AreEqual("1211", Program.SolvePartA("21", 1));
            Assert.AreEqual("111221", Program.SolvePartA("1211", 1));
            Assert.AreEqual("312211", Program.SolvePartA("111221", 1));

            Assert.AreEqual("312211", Program.SolvePartA("1", 5));
            Assert.AreEqual("13112221", Program.SolvePartA("1", 6));
        }
    }
}