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
            Assert.AreEqual(3, Program.GetCodeFor(topLeftCode, 2, 1, x => x+1));
            Assert.AreEqual(6, Program.GetCodeFor(topLeftCode, 3, 1, x => x+1));
            Assert.AreEqual(10, Program.GetCodeFor(topLeftCode, 4, 1, x => x+1));
            Assert.AreEqual(15, Program.GetCodeFor(topLeftCode, 5, 1, x => x+1));
            Assert.AreEqual(21, Program.GetCodeFor(topLeftCode, 6, 1, x => x+1));

            // lazy spot checking now
            Assert.AreEqual(2, Program.GetCodeFor(topLeftCode, 1, 2, x => x+1));
            Assert.AreEqual(20, Program.GetCodeFor(topLeftCode, 5, 2, x => x+1));

            Assert.AreEqual(4, Program.GetCodeFor(topLeftCode, 1, 3, x => x+1));
            Assert.AreEqual(19, Program.GetCodeFor(topLeftCode, 4, 3, x => x+1));

            Assert.AreEqual(16, Program.GetCodeFor(topLeftCode, 1, 6, x => x+1));
        }
    }
}