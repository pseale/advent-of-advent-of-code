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
        return -1;
    }
}
