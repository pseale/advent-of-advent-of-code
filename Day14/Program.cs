using System;
using System.IO;

namespace Day14
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input, 2503);
            Console.WriteLine($"Total change in happiness for the optimal seating arrangement: {partA}");
        }

        private static int SolvePartA(string input, int seconds)
        {
            return -1;
        }

        public static int DistanceTraveled(string line, int seconds)
        {
            return -1;
        }
    }
}