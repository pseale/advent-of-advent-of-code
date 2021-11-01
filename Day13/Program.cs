using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Total change in happiness for the optimal seating arrangement: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"Total change in happiness, including yourself: {partB}");
        }

        public static int SolvePartA(string input)
        {
            var affinities = GetAffinitiesFromInput(input);

            return Solve(affinities);
        }

        private static int SolvePartB(string input)
        {
            var guestOnlyAffinities = GetAffinitiesFromInput(input);
            var attendees = guestOnlyAffinities.Select(x => x.From).Distinct().ToArray();
            var newAffinities = attendees.Select(x => new Affinity("it's me, ur brother", x, 0));
            var affinities = guestOnlyAffinities.Concat(newAffinities).ToList();

            return Solve(affinities);
        }

        private static int Solve(List<Affinity> affinities)
        {
            var seatingHappiness = new Dictionary<string, int>();
            foreach (var affinity in affinities)
            {
                var key = GetKey(affinity.From, affinity.To);
                if (seatingHappiness.ContainsKey(key))
                    seatingHappiness[key] += affinity.Happiness;
                else
                    seatingHappiness.Add(key, affinity.Happiness);
            }

            var all = affinities.Select(x => x.From).Distinct().ToArray();
            var seatingArrangements = Permutate(all);

            var combinations = (List<Combination>) seatingArrangements
                .Select(x => new Combination(x, CalculateHappiness(seatingHappiness, x)))
                .ToList();
            return combinations.OrderByDescending(x => x.Happiness).First().Happiness;
        }

        private static List<Affinity> GetAffinitiesFromInput(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var affinities = new List<Affinity>();
            foreach (var line in lines)
            {
                var words = line.Split(" ");
                var from = words[0];
                var to = words[10].TrimEnd('.');
                var happiness = words[2] == "gain" ? int.Parse(words[3]) : -1 * int.Parse(words[3]);
                affinities.Add(new Affinity(@from, to, happiness));
            }

            return affinities;
        }

        private static string GetKey(string from, string to)
        {
            var names = new List<string>(new[] {from, to});
            names.Sort(); // mutates the list
            var key = $"{names[0]}-{names[1]}";
            return key;
        }

        // As a form of protest, I've gone through the effort to disable everything
        // that complained. There were many many complaints.
        //
        // ReSharper disable IdentifierTypo
        // ReSharper disable PossibleMultipleEnumeration
        //
        // Harvested from https://codereview.stackexchange.com/a/226816
        private static IEnumerable<T[]> Permutate<T>(IEnumerable<T> source)
        {
            return Permutate(source, Enumerable.Empty<T>());

            // ReSharper disable once LocalFunctionHidesMethod
            IEnumerable<T[]> Permutate(IEnumerable<T> reminder, IEnumerable<T> prefix)
            {
                return !reminder.Any()
                    ? new[] {prefix.ToArray()}
                    : reminder.SelectMany((c, i) => Permutate(
                        reminder.Take(i).Concat(reminder.Skip(i + 1)).ToArray(),
                        prefix.Append(c)));
            }
        }
        // ReSharper restore PossibleMultipleEnumeration

        private static int CalculateHappiness(Dictionary<string, int> netHappiness, string[] seatingArrangement)
        {
            var sum = 0;

            for (var i = 0; i < seatingArrangement.Length; i++)
            {
                var previous = i == 0 ? seatingArrangement.Length - 1 : i - 1;
                sum += netHappiness[GetKey(seatingArrangement[previous], seatingArrangement[i])];
            }

            return sum;
        }
    }

    public record Combination(string[] SeatingOrder, int Happiness);

    public record Affinity(string From, string To, int Happiness);
}