namespace Day07;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Minimum fuel spent: {partA}");

        var partB = SolvePartB(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Minimum fuel spent: {partB}");
    }

    public static int SolvePartA(string input)
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
            fuelCosts[i] = CalculateFuelCosts(crabSubmarines, i);
        }

        return fuelCosts.Min();
    }

    private static int CalculateFuelCosts(List<int> crabSubmarines, int horizontalPosition)
    {
        return crabSubmarines
            .Select(submarine => Math.Abs(submarine - horizontalPosition))
            .Sum();
    }

    public static int SolvePartB(string input)
    {
        return -1;
    }
}
