using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var packages = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x))
                .ToArray();

            if (packages.Sum() % 3 != 0)
                throw new Exception($"Packages should have been divisible by 3. Packages: {packages.Length}");

            var targetWeight = packages.Sum() / 3;
            var loadouts = new List<BalancedSleighLoadout>();

            // get all combinations of set #1 - note it must have the correct weight
            var combinations1 = Combinations(packages, targetWeight);
            // get all combinations of set #2
            foreach (var c1 in combinations1)
            {
                var remaining = packages.Except(c1).ToArray();
                var combinations2 = Combinations(remaining, targetWeight);
                // get all combinations of set #3
                foreach (var c2 in combinations2)
                {
                    var c3 = remaining.Except(c2).ToArray();
                    var sorted = new[] {c1, c2, c3}
                        .OrderBy(x => x.Length)
                        .ToArray();
                    var quantumEntanglement = sorted[0].Aggregate(1, (acc, x) => acc * x);

                    loadouts.Add(new BalancedSleighLoadout(sorted[0], sorted[1], sorted[2], quantumEntanglement));
                }
            }

            var idealLoadout = loadouts
                .OrderBy(x => x.Group1.Length)
                .ThenBy(x => x.QuantumEntanglement)
                .First();

            return idealLoadout.QuantumEntanglement;
        }

        // Adapted from https://stackoverflow.com/a/57058345
        public static IEnumerable<int[]> Combinations(int[] source, int targetWeight)
        {
            if (null == source)
                throw new ArgumentNullException(nameof(source));

            return Enumerable
                .Range(0, 1 << source.Length)
                .Select(index => source
                    .Where((v, i) => (index & (1 << i)) != 0)
                    .ToArray()
                    )
                .Where(x => x.Sum() == targetWeight);
        }
    }

    public record BalancedSleighLoadout(int[] Group1, int[] Group2, int[] Group3, int QuantumEntanglement);
}