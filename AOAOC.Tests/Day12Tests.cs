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

        [Test]
        public void PartB()
        {
            // the test cases from part A should count the same
            Assert.AreEqual(6, Program.SolvePartB("[1,2,3]"));
            Assert.AreEqual(6, Program.SolvePartB("{\"a\":2,\"b\":4}"));

            Assert.AreEqual(3, Program.SolvePartB("[[[3]]]"));
            Assert.AreEqual(3, Program.SolvePartB("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.AreEqual(0, Program.SolvePartB("{\"a\":[-1,1]}"));
            Assert.AreEqual(0, Program.SolvePartB("[-1,{\"a\":1}]"));
            Assert.AreEqual(0, Program.SolvePartB("[]"));
            Assert.AreEqual(0, Program.SolvePartB("{}"));

            // additional cases for handling 'red'
            Assert.AreEqual(4, Program.SolvePartB("[1,{\"c\":\"red\",\"b\":2},3]"));
            Assert.AreEqual(0, Program.SolvePartB("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}"));
            Assert.AreEqual(6, Program.SolvePartB("[1,\"red\",5]"));
        }

    }
}