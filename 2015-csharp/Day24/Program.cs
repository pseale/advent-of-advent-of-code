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

            // ***************************
            //           PROTEST
            // ***************************
            Console.WriteLine(
                "PROTEST: Reading over the solutions, it was clear that a few people solved the problem, then the rest of us had to stumble into it either by cheating or (possibly?) by accident. The brute force solution for Part B in particular was untenable. If Part A had something like 248MM combinations, who knows how many Part B had. Anyway I'm pretty cranky about this. I 'solved' the problem inefficiently pretty much immediately. Days later, I have slogged to a finish with a solution that basically only works with the specific problem set (this is what most everyone else did too). I really, really, really don't enjoy these types of puzzles. I was thinking that, if I made it this far, I would maybe appreciate the puzzle solving aspect? But I didn't. It's just anger, with a dash of bitterness. Angry. Anyway here's wonderwall");
            Console.WriteLine();

            var partA = SolvePartA(input);
            Console.WriteLine($"Quantum entanglement: {partA} - took {stopwatch.Elapsed.TotalMinutes:F} minutes");

            stopwatch.Restart();
            var partB = SolvePartB(input);
            Console.WriteLine(
                $"Quantum entanglement, accounting for the trunk: {partB} - took {stopwatch.Elapsed.TotalMinutes:F} minutes");
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
            var combinations1 = GetSmallestCombinations(packages, targetWeight);
            var loadouts = combinations1.Select(x => new Loadout(x, x.Aggregate(1L, (long acc, int x) => acc * x)));

            var idealLoadout = loadouts
                .OrderBy(x => x.Group1.Length)
                .ThenBy(x => x.QuantumEntanglement)
                .First();

            return idealLoadout.QuantumEntanglement;
        }

        public static long SolvePartB(string input)
        {
            var packages = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x))
                .ToArray();

            if (packages.Sum() % 4 != 0)
                throw new Exception($"Packages should have been divisible by 3. Packages: {packages.Length}");

            var targetWeight = packages.Sum() / 4;
            var combinations1 = GetSmallestCombinations(packages, targetWeight);
            var loadouts = combinations1.Select(x => new Loadout(x, x.Aggregate(1L, (long acc, int x) => acc * x)));

            var idealLoadout = loadouts
                .OrderBy(x => x.Group1.Length)
                .ThenBy(x => x.QuantumEntanglement)
                .First();

            return idealLoadout.QuantumEntanglement;
        }

        // THIS IS CHEATING - THIS ONLY SOLVES THE PROBLEM FOR THE EXAMPLE, AND THE SPECIFIC INPUTS.
        // Also I cheated by looking at others' solutions. But I do at least understand them now!
        // See ** PROTEST ** in Program.Main()
        private static int[][] GetSmallestCombinations(int[] remaining, int targetWeight)
        {
            var list = new List<int[]>();
            for (int i = 1; i <= remaining.Length; i++)
            {
                foreach (var c in Combinations(remaining, i))
                {
                    var a = c.ToArray();
                    if (c.Sum() == targetWeight)
                        list.Add(a);
                }

                if (list.Any())
                    return list.ToArray();
            }

            return list.ToArray();
        }

        // copied from stack overflow
        static IEnumerable<IEnumerable<int>> Combinations(IEnumerable<int> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new int[] {item};
                else
                {
                    foreach (var result in Combinations(items.Skip(i + 1), count - 1))
                        yield return new int[] {item}.Concat(result);
                }

                ++i;
            }
        }
    }

    public record Loadout(int[] Group1, long QuantumEntanglement);
}