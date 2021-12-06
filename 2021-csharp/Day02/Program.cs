// See https://aka.ms/new-console-template for more information

namespace Day02;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Multiplied final horizontal position by final depth: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Multiplied final horizontal position by final depth (using new interpretation): {partB}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var instructions = new List<SubmarineInstruction>();
        foreach (var line in lines)
        {
            var words = line.Split(" ");
            instructions.Add(new SubmarineInstruction(words[0], int.Parse(words[1])));
        }

        var depth = 0;
        var horizontalPosition = 0;
        foreach (var instruction in instructions)
            switch (instruction.Direction)
            {
                case "forward":
                    horizontalPosition += instruction.Units;
                    break;
                case "up":
                    depth -= instruction.Units;
                    if (depth < 0)
                        throw new Exception("Depth should never go negative! Submarines can't fly.");
                    break;
                case "down":
                    depth += instruction.Units;
                    break;
                default:
                    throw new Exception($"Invalid submarine instruction: {instruction.Direction}");
            }

        return depth * horizontalPosition;
    }

    public static int SolvePartB(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var instructions = new List<SubmarineInstruction>();
        foreach (var line in lines)
        {
            var words = line.Split(" ");
            instructions.Add(new SubmarineInstruction(words[0], int.Parse(words[1])));
        }

        var aim = 0;
        var depth = 0;
        var horizontalPosition = 0;
        foreach (var instruction in instructions)
            switch (instruction.Direction)
            {
                case "forward":
                    horizontalPosition += instruction.Units;
                    depth += aim * instruction.Units;
                    if (depth < 0)
                        throw new Exception("Depth should never go negative! Submarines can't fly.");
                    break;
                case "up":
                    aim -= instruction.Units;
                    break;
                case "down":
                    aim += instruction.Units;
                    break;
                default:
                    throw new Exception($"Invalid submarine instruction: {instruction.Direction}");
            }

        return depth * horizontalPosition;
    }

    public record SubmarineInstruction(string Direction, int Units);
}