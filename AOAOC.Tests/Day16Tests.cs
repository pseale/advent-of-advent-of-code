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
            // fine, I'll make my own test cases
            var compounds = Program.ParseCompounds(@"children: 3
                cats: 7
                samoyeds: 2
                pomeranians: 3
                akitas: 0
                vizslas: 0
                goldfish: 5
                trees: 3
                cars: 2
                perfumes: 1".Replace("\n",","));

            // match when only one matches
            Assert.AreEqual(1, Program.SolvePartA("Sue 1: cats: 7\nSue 777: cats: 6"));

            // match when everything matches
            Assert.AreEqual(1, Program.SolvePartA("Sue 1: children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1\nSue 777: cats: 6"));

            // match in a realistic situation with 3 known properties, and 2 of which match for most Sues
            Assert.AreEqual(1, Program.SolvePartA("Sue 1: cats: 7, samoyeds: 2, pomeranians: 3\nSue 777: cats: 6, akitas: 0, vizslas: 0"));
        }
    }
}