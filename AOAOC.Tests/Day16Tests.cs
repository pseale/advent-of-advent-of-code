using Day16;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day16Tests
    {
        [Test]
        public void PartA()
        {
            var compounds = Program.GetMfcsamOutput();

            // match in a realistic situation with 3 known properties, and 2 of which match for most Sues
            Assert.AreEqual(1, Program.SolvePartA("Sue 1: cats: 7, samoyeds: 2, pomeranians: 3\nSue 777: cats: 6, akitas: 0, vizslas: 0"));
        }

        [Test]
        public void PartB()
        {
            var compounds = Program.GetMfcsamOutput();

            // match with a greater than reading, a less than reading, and a direct reading
            Assert.AreEqual(1, Program.SolvePartB("Sue 1: cats: 8, pomeranians: 2, samoyeds: 2\nSue 777: cats: 6, akitas: 0, vizslas: 0"));
        }
    }
}