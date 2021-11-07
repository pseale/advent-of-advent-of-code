using System.Collections.Generic;
using Day17;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day17Tests
    {
        [Test]
        public void PartA()
        {
            var input = "20 \n 15 \n 10 \n 5 \n 5";

            Assert.AreEqual(4, Program.SolvePartA(input, 20));
        }

        [Test]
        public void ShouldWorkWithSingleContainer()
        {
            var input = "20";

            Assert.AreEqual(1, Program.SolvePartA(input, 20));
        }

        [Test]
        public void ShouldWorkWithSingleContainerThatFits()
        {
            // only the 20 liter container will fit
            var input = "20 \n 30 \n 40 \n 50";

            Assert.AreEqual(1, Program.SolvePartA(input, 20));
        }

        [Test]
        public void ShouldWorkWithTwoContainers()
        {
            var input = new List<int>() { 10, 10 };

            CollectionAssert.AreEqual(new List<string>() {"-0-1"}, Program.Combinations(input, 20));
        }
    }
}