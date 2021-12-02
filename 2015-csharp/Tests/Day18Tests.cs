using System.Collections.Generic;
using Day18;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day18Tests
    {
        [Test]
        public void PartA()
        {
            var input = @".#.#.#
                          ...##.
                          #....#
                          ..#...
                          #.#..#
                          ####..";

            var (frames, lit) = Day18Solution.GetFrames(input, 4, false);

            var expectedFrames = new List<string>() {
@".#.#.#
...##.
#....#
..#...
#.#..#
####..",


@"..##..
..##.#
...##.
......
#.....
#.##..",

@"..###.
......
..###.
......
.#....
.#....",

@"...#..
......
...#..
..##..
......
......",

@"......
......
..##..
..##..
......
......"
            };

            CollectionAssert.AreEqual(expectedFrames, frames);
            Assert.AreEqual(4, lit);
        }
    }
}