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
            var sues = Parse(input);

            var mfcsamOutput = GetMfcsamOutput();
            return sues.Single(x => Matches(mfcsamOutput, x)).SueNumber;
        }

        private static bool Matches(Dictionary<string,int> compounds, Sue sue)
        {
            return sue.Compounds.All(compound =>
                compounds.ContainsKey(compound.Key) && compounds[compound.Key] == compound.Value);
        }

        private static List<Sue> Parse(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var parsed = new List<Sue>();
            foreach (var line in lines)
            {
                var words = line.Split(" ");
                var sueNumber = int.Parse(words[1].TrimEnd(':'));
                var compounds = new Dictionary<string, int>();
                // Apology: late is the hour (I could have done something to make this dynamic)
                compounds.Add(words[2].TrimEnd(':'), int.Parse(words[3].TrimEnd(',')));
                compounds.Add(words[4].TrimEnd(':'), int.Parse(words[5].TrimEnd(',')));
                compounds.Add(words[6].TrimEnd(':'), int.Parse(words[7].TrimEnd(',')));
                parsed.Add(new Sue(sueNumber, compounds));
            }

            return parsed;
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

        public static Dictionary<string, int> GetMfcsamOutput()
        {
            var compounds = new Dictionary<string, int>();

            // ReSharper disable StringLiteralTypo
            var chunks = @"children: 3, cats: 7, samoyeds: 2, pomeranians: 3, akitas: 0, vizslas: 0, goldfish: 5, trees: 3, cars: 2, perfumes: 1"
                .Split(",");
            // ReSharper restore StringLiteralTypo

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