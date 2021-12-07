namespace Day05;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Points where at least two lines overlap: {partA}");
    }

    public static int SolvePartA(string input)
    {
        return -1;
    }
}
