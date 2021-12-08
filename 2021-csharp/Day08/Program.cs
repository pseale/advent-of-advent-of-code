namespace Day08;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"digits 1,4,7,8 appear this many times in the outputValue: {partA}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var entries = lines
            .Select(x => x.Split(" | ")[1].Split(" "))
            .ToArray();

        var count = 0;
        foreach (var entry in entries)
        {
            count += entry.Where(x => x.Length is 2 or 3 or 4 or 7).Count();
        }
        return count;
    }
}