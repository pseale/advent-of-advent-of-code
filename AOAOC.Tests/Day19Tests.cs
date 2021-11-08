using Day19;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day19Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"H => HO
                          H => OH
                          O => HH

                          HOH";

            Assert.AreEqual(4, Program.SolvePartA(input));
        }
    }
}