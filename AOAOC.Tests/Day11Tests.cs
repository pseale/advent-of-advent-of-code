using Day11;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day11Tests
    {
        [Test]
        public void PartA()
        {
            // end-to-end
            Assert.AreEqual("abcdffaa", Program.SolvePartA("abcdefgh"));
            Assert.AreEqual("ghjaabcc", Program.SolvePartA("ghijklmn"));
        }
    }
}