using System;
using System.IO;

namespace Day17
{
    public static class Program
    {

        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Combinations of containers: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }
    }
}