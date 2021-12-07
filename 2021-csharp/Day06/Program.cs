namespace Day06;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Lanternfish after 80 days: {partA}");
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
}