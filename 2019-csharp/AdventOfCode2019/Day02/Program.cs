namespace Day02;

public static class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllText("input.txt");
        var partA = SolvePartA(input);
        Console.WriteLine($"Value left at position 0: {partA}");
        // Console.WriteLine($"100 * noun + verb: {partA}");
    }

    // apology: Advent of Code is all fake problem solving, so I'm justified in doing whatever I want.
    // I'm aware that this apology is perhaps not very strong. Just be aware that I am aware that an
    // apology is in order. But this is all you get.
    public static int SolvePartA(string input)
    {
        var memory = input
             .Split(",")
             .Select(x => x.Trim())
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .Select(x => int.Parse(x))
             .ToArray();

        memory[1] = 12;
        memory[2] = 2;

        var modifiedInputWith1202ProgramAlarm = string.Join(",", memory);

        var finalMemory = ExecuteIntcode(modifiedInputWith1202ProgramAlarm);
        return finalMemory[0];
    }

    public static int[] ExecuteIntcode(string input)
    {
        var memory = input
             .Split(",")
             .Select(x => x.Trim())
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .Select(x => int.Parse(x))
             .ToArray();

        int position = 0;

        ExecuteIntcodeLoop(memory, position);

        return memory;
    }

    private static void ExecuteIntcodeLoop(int[] memory, int position)
    {
        while (true)
        {
            var opcode = memory[position];
            switch (opcode)
            {
                case 1:
                    var addPosition1 = memory[position + 1];
                    var addPosition2 = memory[position + 2];
                    var addOutputPosition = memory[position + 3];
                    memory[addOutputPosition] = memory[addPosition1] + memory[addPosition2];
                    position += 4;
                    break;
                case 2:
                    var multiplyPosition1 = memory[position + 1];
                    var multiplyPosition2 = memory[position + 2];
                    var multiplyOutputPosition = memory[position + 3];
                    memory[multiplyOutputPosition] = memory[multiplyPosition1] * memory[multiplyPosition2];
                    position += 4;
                    break;
                case 99:
                    return;
                default:
                    throw new NotImplementedException($"Opcode {opcode} at position {position}");
            }
        }
    }
}