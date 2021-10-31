using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Difference between characters of code and characters in memory: {partA}");
        }

        public static int SolvePartA(string input)
        {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var legs = new List<Leg>();
        foreach (var line in lines)
        {
            var words = line.Split(" ");
            legs.Add(new Leg(words[0],words[2], int.Parse(words[4])));
        }

        var reversedLegs = legs.Select(x => new Leg(x.Finish, x.Start, x.Distance)).ToArray();
        legs.AddRange(reversedLegs);

        var combinations = GetAllCombinations(legs);
        var sums = combinations.Select(x => x.Sum(leg => leg.Distance));
        return sums.Min();
    }

        private static IEnumerable<List<Leg>> GetAllCombinations(List<Leg> legs)
        {
            var combinations = new List<List<Leg>>();
            var startLocation = legs.Select(x => x.Start).Distinct().ToArray();

            foreach (var start in startLocation)
            {
                var visited = new[] {start};
                combinations.AddRange(GetCombinationsFor(legs, visited));
            }

            return combinations;
        }

        private static IEnumerable<List<Leg>> GetCombinationsFor(List<Leg> legs, string[] visited)
        {
            if (visited.Length == legs.Select(x => x.Start).Distinct().Count()) // apology: inefficient
            {
                var route = new List<Leg>();
                for (int i = 0; i < visited.Length - 1; i++)
                {
                    route.Add(legs.Single(x => x.Start == visited[i] && x.Finish == visited[i+1]));
                }

                return new[] {route};
            }

            var combinations = new List<List<Leg>>();
            var destinations = GetAllPossibleDestinations(legs, visited);
            foreach (var location in destinations)
            {
                var legsForThisRoute = visited.Concat(new [] {location}).ToArray();
                combinations.AddRange(GetCombinationsFor(legs, legsForThisRoute));
            }

            return combinations;
        }

        private static IEnumerable<string> GetAllPossibleDestinations(List<Leg> legs, string[] visited)
        {
            return legs.Where(x => !visited.Contains(x.Finish))
                .Where(x => visited[^1] == x.Start)
                .Select(x => x.Finish);
        }
    }

    public record Leg(string Start, string Finish, int Distance);
}