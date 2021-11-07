using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    public static class Day18Solution
    {
        public static List<string> GetFrames(string input)
        {
            var lines = input.Split("`n")
                .Select(x => x.Trim())
                .ToArray();

            return new List<string>();
        }

        public static List<string> GetDummyFrames()
        {
            return new List<string>()
            {
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
        }
    }
}