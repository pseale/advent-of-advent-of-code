using Day23;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day23Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"inc a
                          jio a, +2
                          tpl a
                          inc a";

            var result = Program.Solve(input, "a", 0);

            Assert.AreEqual(2, result);
        }
    }
}