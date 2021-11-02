using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Aunt Sue #: {partA}");
        }

        public static int SolvePartA(string input)
        {
            return -1;
        }

        public static Dictionary<string, int> ParseCompounds(string input)
        {
            var compounds = new Dictionary<string, int>();

            var chunks = input.Split(",");
            foreach (var chunk in chunks)
            {
                var words = chunk.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                var name = words[0].TrimEnd(':').Trim();
                var value = int.Parse(words[1].Trim());
                compounds.Add(name, value);
            }

            return compounds;
        }
    }

    public record Sue(int SueNumber, Dictionary<string, int> Compounds);
}