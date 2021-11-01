using System;
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
            var interval = (cycle.FlyingSeconds + cycle.RestingSeconds);
            var fullCycles = seconds / interval;
            var fullCyclesDistance = fullCycles * cycle.FlyingSeconds * cycle.FlyingSpeed;

            // with the remaining time, apply remaining flying time (up to max flying time)
            var remainder = seconds % interval;
            var partialCycleFlightTime = Math.Min(cycle.FlyingSeconds, remainder);
            var partialCycleDistance = partialCycleFlightTime * cycle.FlyingSpeed;

            return fullCyclesDistance + partialCycleDistance;
        }

        private static FlyingRestingCycle GetCycle(string line)
        {
            var words = line.Split(" ");
            return new FlyingRestingCycle(
                words[0],
                int.Parse(words[3]),
                int.Parse(words[6]),
                int.Parse(words[13]));
        }
    }

    record FlyingRestingCycle(string Reindeer, int FlyingSpeed, int FlyingSeconds, int RestingSeconds);
}