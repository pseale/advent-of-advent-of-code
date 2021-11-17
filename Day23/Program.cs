using System;
using System.IO;

namespace Day23
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input, "b");
            Console.WriteLine($"Value of register b: {partA}");
        }

        public static int SolvePartA(string input, string register)
        {
            return -1;
        }
    }
}