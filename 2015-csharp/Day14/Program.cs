using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input, 2503);
            Console.WriteLine($"Winning reindeer has traveled: {partA}");

            var partB = SolvePartB(input, 2503);
            Console.WriteLine($"Winning reindeer has this many points: {partB}");
        }

        private static int SolvePartA(string input, int seconds)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var distances = lines.Select(line => DistanceTraveled(line, seconds));
            return distances.Max();
        }

        public static int DistanceTraveled(string line, int seconds)
        {
            var cycle = GetCycle(line);

            // count how many FULL cycles
            return DistanceTraveledInternal(cycle, seconds);
        }

        // apology: I don't like this name
        private static int DistanceTraveledInternal(FlyingRestingCycle cycle, int seconds)
        {
            var interval = (cycle.FlyingSeconds + cycle.RestingSeconds);
            var fullCycles = seconds / interval;
            var fullCyclesDistance = fullCycles * cycle.FlyingSeconds * cycle.FlyingSpeed;

            // with the remaining time, apply remaining flying time (up to max flying time)
            var remainder = seconds % interval;
            var partialCycleFlightTime = Math.Min(cycle.FlyingSeconds, remainder);
            var partialCycleDistance = partialCycleFlightTime * cycle.FlyingSpeed;

            return fullCyclesDistance + partialCycleDistance;
        }

        private static int SolvePartB(string input, int seconds)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            var cycles = lines.Select(line => GetCycle(line)).ToArray();

            var points = PointsScored(cycles, seconds);

            return points.Max(x => x.Value);
        }

        public static Dictionary<string, int> PointsScored(FlyingRestingCycle[] cycles, int seconds)
        {
            var points = new Dictionary<string, int>();
            foreach (var cycle in cycles)
                points.Add(cycle.Reindeer, 0);

            for (int i = 0; i < seconds; i++)
            {
                var distances = cycles.
                    Select(x => new { Reindeer = x.Reindeer, Distance = DistanceTraveledInternal(x, i + 1) })
                    .ToArray();

                var winners = distances.GroupBy(x => x.Distance)
                    .OrderByDescending(x => x.Key)
                    .First();

                foreach (var winner in winners)
                    points[winner.Reindeer]++;
            }

            return points;
        }

        public static FlyingRestingCycle GetCycle(string line)
        {
            var words = line.Split(" ");
            return new FlyingRestingCycle(
                words[0],
                int.Parse(words[3]),
                int.Parse(words[6]),
                int.Parse(words[13]));
        }
    }

    public record FlyingRestingCycle(string Reindeer, int FlyingSpeed, int FlyingSeconds, int RestingSeconds);
}