using Day08;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day08Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(2 - 0, Program.SolvePartA(Quote("")));
            Assert.AreEqual(5 - 3, Program.SolvePartA(Quote("abc")));
            Assert.AreEqual(10 - 7, Program.SolvePartA(Quote("aaa\\\"aaa"))); // aaa\"aaa
            Assert.AreEqual(6 - 1, Program.SolvePartA(Quote("\\x27"))); // \x27
        }

        private string Quote(string s)
        {
            return '"' + s + '"';
        }
    }
}