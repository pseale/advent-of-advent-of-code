using System;
using System.IO;

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
            return -1;
        }
    }
}