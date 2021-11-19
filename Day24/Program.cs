using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day24
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Quantum entanglement of the first group of packages: {partA} - took {stopwatch.Elapsed.TotalMinutes:F} minutes");
        }

        public static long SolvePartA(string input)
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

            var (minimum, maximum) = GetBounds(packages, targetWeight);
            // get all combinations of set #1 - note any combination must have the correct weight
            var combinations1 = GetAllCombinations(packages, minimum, maximum, targetWeight);

            // get all combinations of set #2
            foreach (var c1 in combinations1)
            {
                var remaining = packages.Except(c1).ToArray();
                var combinations2 = combinations1
                    .Where(combination => combination.All(x => remaining.Contains(x)))
                    .ToArray();
                // get all combinations of set #3
                foreach (var c2 in combinations2)
                {
                    var c3 = remaining.Except(c2).ToArray();
                    var sorted = new[] {c1.ToArray(), c2.ToArray(), c3.ToArray()}
                        .OrderBy(x => x.Length)
                        .ToArray();
                    var quantumEntanglement = sorted[0].Aggregate(1L, (long acc, int x) => acc * x);

                    loadouts.Add(new BalancedSleighLoadout(sorted[0], sorted[1], sorted[2], quantumEntanglement));
                }
            }

            var idealLoadout = loadouts
                .OrderBy(x => x.Group1.Length)
                .ThenBy(x => x.QuantumEntanglement)
                .First();

            return idealLoadout.QuantumEntanglement;
        }

        private static int[][] GetAllCombinations(int[] remaining, int minimum, int maximum, int targetWeight)
        {
            var list = new List<int[]>();
            for (int i = 6; i <= 8; i++)
            {
                foreach (var c in Combinations(remaining, i))
                {
                    var a = c.ToArray();
                    if (c.Sum() == targetWeight)
                        list.Add(a);
                }
            }

            return list.ToArray();
        }
        static IEnumerable<IEnumerable<int>> Combinations(IEnumerable<int> items, int count)
        {
            int i = 0;
            foreach(var item in items)
            {
                if(count == 1)
                    yield return new int[] { item };
                else
                {
                    foreach(var result in Combinations(items.Skip(i + 1), count - 1))
                        yield return new int[] { item }.Concat(result);
                }

                ++i;
            }
        }
        private static (int, int) GetBounds(int[] packages, int targetWeight)
        {
            var sorted = packages.OrderBy(x1 => x1).ToArray();
            var smallPackages = sorted
                .Select((x, i) => sorted.Take(i + 1).Sum())
                .Where(x => x <= targetWeight);
            var maximum = smallPackages.Count();

            // var sortedDescending = packages.OrderByDescending(x1 => x1).ToArray();
            // var largePackages = sortedDescending
            //     .Select((x, i) => sortedDescending.Take(i + 1).Sum())
            //     .Where(x => x >= targetWeight);
            var minimum = 5; //largePackages.Count();

            return (minimum, maximum);
        }

    }

    public record BalancedSleighLoadout(int[] Group1, int[] Group2, int[] Group3, long QuantumEntanglement);
}