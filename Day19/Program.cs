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

        public static long SolvePartA(string input)
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
                    replacements.Add(key, new List<string>() { words[2] });
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

            // int options = 0;
            // foreach (var token in tokens)
            // {
            //     if (replacements.ContainsKey(token))
            //         options += replacements[token].Count;
            // }
            //
            // return options;
            var combinations = Combinations(replacements, tokens, Array.Empty<string>());
            var distinct = combinations.ToArray().Distinct().ToArray();
            return distinct.Length;
        }

        private static IEnumerable<string> Combinations(Dictionary<string,List<string>> replacements, List<string> tokens, string[] currentChoices)
        {
            if (currentChoices.Length == tokens.Count)
                return new[] { string.Join("", currentChoices) };

            var choiceIndex = currentChoices.Length;
            var atom = tokens[choiceIndex];
            var list = new List<string>();
            foreach (var choice in GetReplacements(replacements, atom))
            {
                list.AddRange(Combinations(replacements, tokens, currentChoices.Append(choice).ToArray()));
            }

            return list;
        }

        private static List<string> GetReplacements(Dictionary<string, List<string>> replacements, string atom)
        {
            if (replacements.ContainsKey(atom))
                return replacements[atom];

            return new List<string>() {atom};
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