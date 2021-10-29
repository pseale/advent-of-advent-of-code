using Day09;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day09Tests
    {
        [Test]
        public void PartA()
        {
            string distances = @"London to Dublin = 464
                                London to Belfast = 518
                                Dublin to Belfast = 141";

            Assert.AreEqual(605, Program.SolvePartA(distances));
        }

    }
}