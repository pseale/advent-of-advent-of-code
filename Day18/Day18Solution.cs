using System.Collections.Generic;

namespace Day18
{
    public static class Day18Solution
    {
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