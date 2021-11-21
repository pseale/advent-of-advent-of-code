using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Day19
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Distinct molecules: {partA}");
        }

        // apology: I'm pretty aware this function doesn't deserve to exist.
        // Inline it I guess? BIG SHRUG. Welcome to Advent of Code!
        // I'm pretty sure Advent of Code requires all submitted solutions as integer values because
        // it's easy to verify. Anyway, welcome!
        public static long SolvePartA(string input)
        {
            var combinations = GetReplacementMolecules(input);

            var distinct = combinations.Distinct().ToArray();
            return distinct.Length;
        }

        public static List<string> GetReplacementMolecules(string input)
        {
            var replacements = new Dictionary<string, List<string>>();
            var lines = input.Split("\n")
                .Select(x => x.Trim())
                .Where(x => x.Contains(" => "));
            foreach (var line in lines)
            {
                var words = line.Split(" ");
                var key = words[0];
                if (!replacements.ContainsKey(key))
                    replacements.Add(key, new List<string>() {words[2]});
                else
                    replacements[key].Add(words[2]);
            }

            // ReSharper disable once ReplaceWithSingleCallToLast
            var calibrationMolecule = input
                .Split("\n")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Last()
                .Trim();

            var tokens = Tokenize(calibrationMolecule);

            var combinations = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (!replacements.ContainsKey(tokens[i]))
                    continue;

                var left = string.Join("", tokens.Take(i + 1 - 1));
                var right = string.Join("", tokens.Skip(i + 1));

                foreach (var replacement in replacements[tokens[i]])
                    combinations.Add(left + replacement + right);
            }

            return combinations;
        }

        private static List<string> Tokenize(string molecule)
        {
            var tokens = new List<string>();
            int index = 0;

            while (index < molecule.Length)
            {
                // find matching atom
                var match = GetAtomicToken(molecule.Substring(index));

                // add it & move forward
                tokens.Add(match);
                index += match.Length;
            }

            return tokens;
        }

        private static string GetAtomicToken(string molecule)
        {
            if (molecule.Length == 1)
                return molecule;

            // e.g. Mg
            if (molecule[1].ToString().ToLowerInvariant() == molecule[1].ToString())
                return molecule.Substring(0, 2);

            // e.g. C
            return molecule.Substring(0, 1);
        }
    }
}