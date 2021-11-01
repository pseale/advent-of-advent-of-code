using System;
using System.IO;

namespace Day13
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Total change in happiness for the optimal seating arrangement: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }
    }
}