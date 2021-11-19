using Day24;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day24Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"1
                          2
                          3
                          4
                          5
                          7
                          8
                          9
                          10
                          11";

            var result = Program.SolvePartA(input);

            Assert.AreEqual(90, result);
        }
    }
}