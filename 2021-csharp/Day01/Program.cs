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
        }

        public static int SolvePartA(string input)
        {
            var depths = input
                         .Split("\n")
                         .Select(x => x.Trim())
                         .Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(x => int.Parse(x))
                         .ToArray();

            // loop until end - if depth > depth - 1, add to the 'increasing' count
            int increasing = 0;

            for (int i = 1; i < depths.Length; i++)
                if (depths[i] > depths[i - 1])
                    increasing++;

            return increasing;
        }
    }
}