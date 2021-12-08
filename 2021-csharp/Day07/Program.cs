namespace Day07;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = Solve(input, false);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Minimum fuel spent: {partA}");

        var partB = Solve(input, true);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Minimum fuel spent: {partB}");
    }

    public static int Solve(string input, bool variableRateFuelCost)
    {
        // ReSharper disable once ReplaceWithSingleCallToSingle
        var line = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Single();

        var crabSubmarines = line.Split(",")
            .Select(x => int.Parse(x))
            .ToList();
        var horizontalPositions = crabSubmarines.Max() + 1;

        var fuelCosts = new int[horizontalPositions];
        for (int i = 0; i < horizontalPositions; i++)
        {
            fuelCosts[i] = CalculateFuelCosts(crabSubmarines, i, variableRateFuelCost);
        }

        return fuelCosts.Min();
    }

    private static int CalculateFuelCosts(List<int> crabSubmarines, int horizontalPosition, bool variableRateFuelCost)
    {
        if (!variableRateFuelCost)
            return crabSubmarines
                .Select(submarine => Math.Abs(submarine - horizontalPosition))
                .Sum();

        return crabSubmarines
            .Select(submarine =>
            {
                var distance = Math.Abs(submarine - horizontalPosition);
                // harvested formula from https://en.wikipedia.org/wiki/1_%2B_2_%2B_3_%2B_4_%2B_%E2%8B%AF
                return distance * (distance + 1) / 2;
            })
            .Sum();
    }
}
