using Day01;
using NUnit.Framework;

namespace AOAOC.Tests
{
    public class Day01Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(0, Program.SolvePartA("(())"));
            Assert.AreEqual(0, Program.SolvePartA("()()"));
            
            Assert.AreEqual(3, Program.SolvePartA("((("));
            Assert.AreEqual(3, Program.SolvePartA("(()(()("));

            Assert.AreEqual(3, Program.SolvePartA("))((((("));
            
            Assert.AreEqual(-1, Program.SolvePartA("())"));
            Assert.AreEqual(-1, Program.SolvePartA("))("));
            
            Assert.AreEqual(-3, Program.SolvePartA(")))"));
            Assert.AreEqual(-3, Program.SolvePartA(")())())"));
        }

        [Test]
        public void PartB()
        {
            Assert.AreEqual(1, Program.SolvePartB(")"));
            Assert.AreEqual(5, Program.SolvePartB("()())"));
        }
    }
}