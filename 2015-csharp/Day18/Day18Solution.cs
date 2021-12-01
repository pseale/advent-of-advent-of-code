using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day18
{
    public static class Day18Solution
    {
        public static (List<string>, int) GetFrames(string input, int frameCount, bool cornersRemainLit)
        {
            var lines = input.Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            var size = lines.Length;

            var frames = new List<string>();

            var firstFrame = new char[size, size]; // cols, rows

            // seed the first frame
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++) // beware jagged arrays
                    firstFrame[col, row] = lines[row][col];
            frames.Add(ConvertToString(firstFrame, size));


            // calculate the rest of the frames
            var currentFrame = firstFrame;
            for (int i = 0; i < frameCount; i++)
            {
                var nextFrame = Iterate(currentFrame, size, cornersRemainLit);
                frames.Add(ConvertToString(nextFrame, size));
                currentFrame = nextFrame;
            }

            // ReSharper disable once ReplaceWithSingleCallToCount
            var lit = frames[^1]
                .ToCharArray()
                .Where(x => x == '#')
                .Count();
            return (frames, lit);
        }

        private static char[,] Iterate(char[,] frame, int size, bool cornersRemainLit)
        {
            var nextFrame = new char[size, size]; // cols, rows

            for (int col = 0; col < size; col++)
            for (int row = 0; row < size; row++)
                nextFrame[col, row] = CalculateNext(frame, size, row, col, cornersRemainLit);

            return nextFrame;
        }

        // hammer out all 8 possible neighbors
        private static char CalculateNext(char[,] frame, int size, int row, int col, bool cornersRemainLit)
        {
            if (cornersRemainLit && (row == 0 || row == size - 1) && (col == 0 || col == size - 1))
                return '#';

            int litNeighbors = 0;
            for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) // don't check myself
                    continue;

                if (IsLit(frame, size, row + y, col + x))
                    litNeighbors++;
            }

            if (frame[col, row] == '#')
                if (litNeighbors is 2 or 3) // APOLOGY: Rider suggested this 'pattern'. Do not hold me accountable for this line of code. Not my idea and not my fault.
                    return '#';
                else
                    return '.';

            if (litNeighbors == 3)
                return '#';

            return '.';
        }

        private static bool IsLit(char[,] frame, int size, int row, int col)
        {
            if (row < 0 || row > size - 1 || col < 0 || col > size - 1)
                return false;

            return frame[col, row] == '#';
        }

        private static string ConvertToString(char[,] frame, int size)
        {
            var sb = new StringBuilder();

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                    sb.Append(frame[col, row]);
                sb.AppendLine();
            }

            return sb.ToString().Trim();
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