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

            var frames = Day18Solution.GetFrames(input);

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
        }
    }
}