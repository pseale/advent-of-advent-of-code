using Day07;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day07Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"123 -> x
                456 -> y
                x AND y -> d
                x OR y -> e
                x LSHIFT 2 -> f
                y RSHIFT 2 -> g
                NOT x -> h
                NOT y -> i";

            var wires = Program.SolvePartA(input);

            Assert.AreEqual(72, wires["d"]);
            Assert.AreEqual(507, wires["e"]);
            Assert.AreEqual(492, wires["f"]);
            Assert.AreEqual(114, wires["g"]);
            Assert.AreEqual(65412, wires["h"]);
            Assert.AreEqual(65079, wires["i"]);
            Assert.AreEqual(123, wires["x"]);
            Assert.AreEqual(456, wires["y"]);
        }

        [Test]
        public void PartASecretRequirementsFromInput()
        {
            var input = @"123 -> x
                x -> y";

            var wires = Program.SolvePartA(input);

            Assert.AreEqual(123, wires["x"]);
            Assert.AreEqual(123, wires["y"]);

        }

    }
}