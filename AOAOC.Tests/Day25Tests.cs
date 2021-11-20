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

            Assert.AreEqual(2, Program.SolvePartA(topLeftCode, 2, 1));
        }
    }
}