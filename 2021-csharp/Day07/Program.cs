namespace Day07;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        // ReSharper disable once StringLiteralTypo
        Console.WriteLine($"Minimum fuel spent: {partA}");
    }

    public static int SolvePartA(string input)
    {
        return -1;
    }
}
