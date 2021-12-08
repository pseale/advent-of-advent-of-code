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
        return -1;
    }
}