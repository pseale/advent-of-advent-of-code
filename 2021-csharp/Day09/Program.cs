namespace Day09;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Sum of the risk levels of all low points: {partA}");
    }

    public static int SolvePartA(string input)
    {
        return -1;
    }
}
