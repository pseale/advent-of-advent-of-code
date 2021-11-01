using System;
using System.IO;

namespace Day12
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.json");

            var partA = SolvePartA(input);
            Console.WriteLine($"Sum of all numbers: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }
    }
}