using System;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputPartA = File.ReadAllText("input-part-a.txt");
            var partA = SolvePartA(inputPartA);
            Console.WriteLine($"Square feet of wrapping paper: {partA}");
        }

        public static int SolvePartA(string input)
        {
            var presents = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => Parse(x))
                .ToArray();

            return presents.Sum(present => CalculateWrappingPaperFor(present.Height, present.Width, present.Length));
        }

        private static int CalculateWrappingPaperFor(int h, int w, int l)
        {
            var side1 = 2 * l * w;
            var side2 = 2 * w * h;
            var side3 = 2 * h * l;
            var smallestSide = Math.Min(Math.Min(side1, side2), side3) / 2;
            return side1 + side2 + side3 + smallestSide;
        }

        private static Present Parse(string input)
        {
            // e.g. 2x3x4
            var dimensions = input.Split("x");
            return new Present(int.Parse(dimensions[0]), int.Parse(dimensions[1]), int.Parse(dimensions[2]));
        }

        private record Present(int Height, int Width, int Length);
    }
}