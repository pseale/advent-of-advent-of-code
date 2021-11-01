using Day12;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day12Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(6, Program.SolvePartA("[1,2,3]"));
            Assert.AreEqual(6, Program.SolvePartA("{\"a\":2,\"b\":4}"));

            Assert.AreEqual(3, Program.SolvePartA("[[[3]]]"));
            Assert.AreEqual(3, Program.SolvePartA("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.AreEqual(0, Program.SolvePartA("{\"a\":[-1,1]}"));
            Assert.AreEqual(0, Program.SolvePartA("[-1,{\"a\":1}]"));
            Assert.AreEqual(0, Program.SolvePartA("[]"));
            Assert.AreEqual(0, Program.SolvePartA("{}"));
        }
    }
}