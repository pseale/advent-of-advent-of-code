using System;
using System.IO;

namespace Day24
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Quantum entanglement of the first group of packages: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }
    }
}