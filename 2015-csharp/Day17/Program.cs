using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day17
{
    public static class Program
    {

        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input, 150);
            Console.WriteLine($"Combinations of containers: {partA}");

            var partB = SolvePartB(input, 150);
            Console.WriteLine($"Combinations of minimum number of containers: {partB}");
        }

        public static int SolvePartA(string input, int target)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var containers = lines.Select(line => int.Parse(line)).ToList();
            return Combinations(containers, target)
                .Count();
        }

        private static int SolvePartB(string input, int target)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var containers = lines.Select(line => int.Parse(line)).ToList();
            var combinations = Combinations(containers, target);

            var minimumContainers = combinations
                .Select(x => x.Split("-"))
                .OrderBy(x => x.Length)
                .First()
                .Length;


            // ReSharper disable once ReplaceWithSingleCallToCount
            var combinationsWithMinimumContainers = combinations
                .Select(x => x.Split("-"))
                .Where(x => x.Length == minimumContainers)
                .Count();

            return combinationsWithMinimumContainers;
        }

        public static IEnumerable<string> Combinations(List<int> containers, int target)
        {
            return CombinationsRecursive(containers, target, "", 0)
                .ToArray()
                .Distinct()
                .ToArray();
        }

        // EDITOR'S NOTE: this one took a lot of effort to get right. I am not accustomed to writing
        // recursive combinatorial algorithms.
        private static IEnumerable<string> CombinationsRecursive(List<int> containers, int target, string partialCombination, int loop)
        {
            if (loop == containers.Count)
            {
                var sum = partialCombination
                    .Split("-")
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Sum(x => containers[int.Parse(x)]);
                if (sum == target)
                    yield return partialCombination;
            }
            else
            {
                // try a combination where we use this container
                var newPartialCombination = $"{partialCombination}-{loop}";
                foreach (var combination in CombinationsRecursive(containers, target, newPartialCombination, loop + 1))
                    yield return combination;

                // try a combination where we do NOT use this container
                foreach (var combination in CombinationsRecursive(containers, target, partialCombination, loop + 1))
                    yield return combination;
            }
        }
    }
}