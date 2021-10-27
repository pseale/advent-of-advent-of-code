using Day05;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day05Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(1, Program.SolvePartA("ugknbfddgicrmopn"));
            Assert.AreEqual(1, Program.SolvePartA("aaa"));

            Assert.AreEqual(0, Program.SolvePartA("jchzalrnumimnmhp"));
            Assert.AreEqual(0, Program.SolvePartA("haegwjzuvuyypxyu"));
            Assert.AreEqual(0, Program.SolvePartA("dvszwmarrgswjxmb"));
        }

        [Test]
        public void PartB()
        {
            Assert.AreEqual(1, Program.SolvePartB("qjhvhtzxzqqjkmpb"));
            Assert.AreEqual(1, Program.SolvePartB("xxyxx"));

            Assert.AreEqual(0, Program.SolvePartB("uurcxstgmygtbstg"));
            Assert.AreEqual(0, Program.SolvePartB("ieodomkazucvgmuy"));
        }
    }
}