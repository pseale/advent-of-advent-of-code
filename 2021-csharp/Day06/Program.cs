namespace Day06;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Lanternfish after 80 days: {partA}");

        var partB = SolvePartB(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Lanternfish after 256 days: {partB}");
    }

    public static int SolvePartA(string input)
    {
        // ReSharper disable once ReplaceWithSingleCallToSingle
        var line = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Single();

        var fish = line.Split(",")
            .Select(x => int.Parse(x))
            .ToList();

        for (var day = 0; day < 80; day++)
        {
            var newFish = new List<int>();
            for (var i = 0; i < fish.Count; i++)
            {
                fish[i]--;
                if (fish[i] < 0)
                {
                    newFish.Add(8);
                    fish[i] = 6;
                }
            }

            fish.AddRange(newFish);
        }

        return fish.Count;
    }

    // Thanks to smab - solved the problem by looking at his solution, then attempting
    // to code it myself AFTER seeing how to solve the problem.
    //
    // See https://github.com/smabuk/AdventOfCode/blob/main/Solutions/2021/Day06.cs
    public static long SolvePartB(string input)
    {
        // ReSharper disable once ReplaceWithSingleCallToSingle
        var line = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Single();

        var fish = line.Split(",")
            .Select(x => int.Parse(x))
            .ToList();

        // count fish by age (internal timer)
        var stuff = Enumerable.Range(0, 9)
            .Select(i => (long)fish.Count(timer => timer == i))
            .ToArray();

        for (var day = 0; day < 256; day++)
        {
            var fishThatSpawned = stuff[0];
            for (int i = 0; i < 9 - 1; i++)
            {
                stuff[i] = stuff[i + 1];
            }

            stuff[6] += fishThatSpawned; // for old fish that have spawned a newborn, reset timer to 6
            stuff[8] = fishThatSpawned;  // for old fish that have spawned a newborn, spawned a newborn fish with timer 8
        }

        return stuff.Sum();
    }
}