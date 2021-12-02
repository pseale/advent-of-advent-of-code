namespace Day05;

public static class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllText("input.txt");
        var partA = SolvePartA(input);
        Console.WriteLine($"Diagnostic code: {partA}");
    }

    private static int SolvePartA(string input)
    {
        return -1;
    }

    public static int[] ExecuteIntcode(string input)
    {
        return new int[0];
    }
}
