using System;
using System.IO;

namespace Day03
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input-part-a.txt");
            int partA = SolvePartA(input);
            Console.WriteLine($"Houses receiving at least one present: {partA}");
        }

        private static int SolvePartA(string input)
        {
            return 0;
        }
    }
}