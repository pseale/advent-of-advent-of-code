using System;
using System.IO;

namespace Day05
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            int partA = SolvePartA(input);
            Console.WriteLine($"Nice strings: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return 0;
        }
    }
}