using Day14;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day14Tests
    {
        [Test]
        public void PartA()
        {
            Assert.AreEqual(14, Program.DistanceTraveled(
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                1));
            Assert.AreEqual(16, Program.DistanceTraveled(
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
                1));

            Assert.AreEqual(140, Program.DistanceTraveled(
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                10));
            Assert.AreEqual(160, Program.DistanceTraveled(
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
                10));

            Assert.AreEqual(140, Program.DistanceTraveled(
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                11));
            Assert.AreEqual(176, Program.DistanceTraveled(
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
                11));

            Assert.AreEqual(140, Program.DistanceTraveled(
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                12));
            Assert.AreEqual(176, Program.DistanceTraveled(
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
                12));

            Assert.AreEqual(1120, Program.DistanceTraveled(
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
                1000));
            Assert.AreEqual(1056, Program.DistanceTraveled(
                "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.",
                1000));
        }

    }
}