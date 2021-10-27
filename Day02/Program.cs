using System;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = File.ReadAllText("input-part-a.txt");
            var partA = SolvePartA(input);
            Console.WriteLine($"Square feet of wrapping paper: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"feet of ribbon: {partB}");
        }

        public static int SolvePartA(string input)
        {
            var presents = Parse(input);

            return presents.Sum(present => CalculateWrappingPaper(present));
        }

        private static Present[] Parse(string input)
        {
            var presents = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x =>
                {
                    // e.g. 2x3x4
                    var dimensions = x.Split("x");
                    return new Present(int.Parse(dimensions[0]), int.Parse(dimensions[1]), int.Parse(dimensions[2]));
                })
                .ToArray();
            return presents;
        }

        private static int CalculateWrappingPaper(Present present)
        {
            var side1 = 2 * present.Length * present.Width;
            var side2 = 2 * present.Width * present.Height;
            var side3 = 2 * present.Height * present.Length;
            var smallestSide = Math.Min(Math.Min(side1, side2), side3) / 2;
            return side1 + side2 + side3 + smallestSide;
        }

        public static int SolvePartB(string input)
        {
            var presents = Parse(input);

            return presents.Sum(x => CalculateFeetOfRibbonFor(x));
        }

        private static int CalculateFeetOfRibbonFor(Present present)
        {
            var perimeter1 = 2 * (present.Length + present.Width);
            var perimeter2 = 2 * (present.Width + present.Height);
            var perimeter3 = 2 * (present.Height + present.Length);

            var smallestPerimeter = Math.Min(Math.Min(perimeter1, perimeter2), perimeter3);
            var ribbon = present.Height * present.Length * present.Width;
            return smallestPerimeter + ribbon;
        }

        private record Present(int Height, int Width, int Length);
    }
}