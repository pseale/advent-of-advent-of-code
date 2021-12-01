using Day01;
using NUnit.Framework;

namespace Tests
{
    public class Day01Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"199
                          200
                          208
                          210
                          200
                          207
                          240
                          269
                          260
                          263";

            Assert.AreEqual(7, Program.SolvePartA(input));
        }

        [Test]
        public void PartB()
        {
            var input = @"199
                          200
                          208
                          210
                          200
                          207
                          240
                          269
                          260
                          263";

            Assert.AreEqual(5, Program.SolvePartB(input));
        }
    }
}