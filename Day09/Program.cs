using System;
using System.IO;

namespace Day09
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Difference between characters of code and characters in memory: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }
    }
}