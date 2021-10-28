using System;
using System.Collections.Generic;
using System.IO;

namespace Day07
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Signal provided to wire a: {partA["a"]}");
        }

        public static Dictionary<string, int> SolvePartA(string input)
        {
            return new Dictionary<string, int>();
        }
    }
}