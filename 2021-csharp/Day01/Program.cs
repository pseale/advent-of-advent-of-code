using System;
using System.IO;
using System.Linq;

namespace Day01
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var partA = SolvePartA(input);
            Console.WriteLine($"Measurements larger than the previous measurement: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"Sums (of measurements) larger than the previous sum: {partB}");
        }

        public static int SolvePartA(string input)
        {
            var depths = input
                         .Split("\n")
                         .Select(x => x.Trim())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(x => int.Parse(x))
                         .ToArray();

            int increasing = 0;

            for (int i = 1; i < depths.Length; i++)
                if (depths[i] > depths[i - 1])
                    increasing++;

            return increasing;
        }

        public static int SolvePartB(string input)
        {
            var depths = input
                         .Split("\n")
                         .Select(x => x.Trim())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(x => int.Parse(x))
                         .ToArray();

            int increasing = 0;

            for (int i = 3; i < depths.Length; i++)
            {
                var previousSum = depths[i - 3] + depths[i - 2] + depths[i - 1];
                var sum = depths[i - 2] + depths[i - 1] + depths[i];
                if (sum > previousSum)
                    increasing++;
            }

            return increasing;
        }
    }
}