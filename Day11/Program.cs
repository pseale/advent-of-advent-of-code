using System;

namespace Day11
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = "cqjxjnds";

            var partA = SolvePartA(input);
            Console.WriteLine($"Santa's next password should be: {partA}");
        }

        public static string SolvePartA(string input)
        {
            return "hunter2";
        }
    }
}