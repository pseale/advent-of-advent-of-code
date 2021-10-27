using Day04;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day04Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(609043, Program.SolvePartA("abcdef"));
            Assert.AreEqual(1048970, Program.SolvePartA("pqrstuv"));
        }
    }
}