// See https://aka.ms/new-console-template for more information

namespace Day02;

public static class Program
{
    static void Main()
    {
        var input = File.ReadAllText("input.txt");
        var partA = SolvePartA(input);
        Console.WriteLine($"Multiplied final horizontal position by final depth: {partA}");
    }

    public static int SolvePartA(string input)
    {
        return -1;
    }
}