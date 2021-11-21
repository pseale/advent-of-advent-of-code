using System.Linq;
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
            var example1 = @"H => HO
                          H => OH
                          O => HH

                          HOH";

            Assert.AreEqual(4, Program.SolvePartA(example1));

            var example2 = @"H => HO
                          H => OH
                          O => HH

                          HOHOHO";
            Assert.AreEqual(7, Program.SolvePartA(example2));
        }

        [Test]
        public void GetReplacementMoleculesShouldWork()
        {
            var example1 = @"H => HO
                          H => OH
                          O => HH

                          HOH";

            var result = Program.GetReplacementMolecules(example1);
            var expected = new[] {"HOOH", "HOHO", "OHOH", "HOOH", "HHHH"};
            CollectionAssert.AreEqual(expected.OrderBy(x => x), result.OrderBy(x => x));
        }
    }
}