using Day25;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day25Tests
    {
        [Test]
        public void PartA()
        {
            var topLeftCode = 1;

            //    | 1   2   3   4   5   6
            // ---+---+---+---+---+---+---+
            // 1 |  1   3   6  10  15  21
            // 2 |  2   5   9  14  20
            // 3 |  4   8  13  19
            // 4 |  7  12  18
            // 5 | 11  17
            // 6 | 16

            // top row (row #1)
            Assert.AreEqual(1, Program.SolvePartA(topLeftCode, 1, 1));
            Assert.AreEqual(3, Program.SolvePartA(topLeftCode, 2, 1));
            Assert.AreEqual(6, Program.SolvePartA(topLeftCode, 3, 1));
            Assert.AreEqual(10, Program.SolvePartA(topLeftCode, 4, 1));
            Assert.AreEqual(15, Program.SolvePartA(topLeftCode, 5, 1));
            Assert.AreEqual(21, Program.SolvePartA(topLeftCode, 6, 1));

            // lazy spot checking now
            Assert.AreEqual(2, Program.SolvePartA(topLeftCode, 1, 2));
            Assert.AreEqual(20, Program.SolvePartA(topLeftCode, 5, 2));

            Assert.AreEqual(4, Program.SolvePartA(topLeftCode, 1, 3));
            Assert.AreEqual(19, Program.SolvePartA(topLeftCode, 4, 3));

            Assert.AreEqual(16, Program.SolvePartA(topLeftCode, 1, 6));
        }
    }
}