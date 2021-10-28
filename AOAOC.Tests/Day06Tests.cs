using Day06;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day06Tests
    {
        [Test]
        public void PartA()
        {
            // making my own test cases, because their test cases involve 1000x1000 matrices
            Assert.AreEqual(0, Program.SolvePartA(""));

            // test very basic use case
            Assert.AreEqual(1, Program.SolvePartA("toggle 0,0 through 0,0"));

            // test that ranges work (very rudimentary)
            Assert.AreEqual(2, Program.SolvePartA("toggle 0,0 through 0,1"));
            Assert.AreEqual(2, Program.SolvePartA("toggle 0,0 through 1,0"));
            Assert.AreEqual(4, Program.SolvePartA("toggle 0,0 through 1,1"));

            // test 'toggle'
            Assert.AreEqual(0, Program.SolvePartA("toggle 0,0 through 0,0\ntoggle 0,0 through 0,0"));

            // test 'turn on'
            Assert.AreEqual(2, Program.SolvePartA("turn on 0,0 through 0,1"));
            Assert.AreEqual(2, Program.SolvePartA("turn on 0,0 through 0,1"));

            // test 'turn off'
            Assert.AreEqual(0, Program.SolvePartA("turn on 0,0 through 0,0\nturn off 0,0 through 0,0"));
        }

        [Test]
        public void PartB()
        {
            // assume ranges still work from Part A

            // test 'toggle'
            Assert.AreEqual(2 + 2, Program.SolvePartB("toggle 0,0 through 0,0\ntoggle 0,0 through 0,0"));

            // test 'turn on'
            Assert.AreEqual(1 + 1, Program.SolvePartB("turn on 0,0 through 0,0\nturn on 0,0 through 0,0"));

            // test 'turn off'
            Assert.AreEqual(1 + 1 - 1, Program.SolvePartB("turn on 0,0 through 0,0\nturn on 0,0 through 0,0\nturn off 0,0 through 0,0"));
        }
    }
}