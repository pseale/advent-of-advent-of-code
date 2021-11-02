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
            var input = @"20 \n 15 \n 10 \n 5 \n 5";

            Assert.AreEqual(4, Program.SolvePartA(input));
        }
    }
}