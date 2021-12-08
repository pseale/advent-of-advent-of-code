namespace Day05;

public static class Program
{
    private static void Main(string[] args)
    {
        var input = File.ReadAllText("input.txt");
        var partA = SolvePartA(input, 1);
        Console.WriteLine($"Diagnostic code: {partA}");

        var partB = SolvePartB(input, 5);
        Console.WriteLine($"Diagnostic code (system ID 5): {partB}");
    }

    private static int SolvePartA(string input, int inputValue)
    {
        var (_, outputValue) = ExecuteIntcode(input, inputValue);
        return outputValue;
    }

    private static int SolvePartB(string input, int inputValue)
    {
        var (_, outputValue) = ExecuteIntcode(input, inputValue);
        return outputValue;
    }

    public static (int[] memory, int outputValue) ExecuteIntcode(string input, int inputValue)
    {
        var memory = input
             .Split(",")
             .Select(x => x.Trim())
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .Select(x => int.Parse(x))
             .ToArray();

        int position = 0;

        var outputValue = ExecuteIntcodeLoop(memory, position, inputValue);

        return (memory, outputValue);
    }

    private static int ExecuteIntcodeLoop(int[] memory, int position, int inputValue)
    {
        int outputValue = -1;

        while (true)
        {
            var rawOpcode = memory[position];
            var opcode = rawOpcode % 100;
            var parameter1Mode = rawOpcode / 100 % 10;
            var parameter2Mode = rawOpcode / 1000 % 10;
            var parameter3Mode = rawOpcode / 10000;

            switch (opcode)
            {
                case 1:
                    var add1 = Get(memory, position + 1, parameter1Mode);
                    var add2 = Get(memory, position + 2, parameter2Mode);
                    var addOutputPosition = Get(memory, position + 3, parameter3Mode, false);
                    memory[addOutputPosition] = add1 + add2;
                    position += 4;
                    break;
                case 2:
                    var multiply1 = Get(memory, position + 1, parameter1Mode);
                    var multiply2 = Get(memory, position + 2, parameter2Mode);
                    var multiplyOutputPosition = Get(memory, position + 3, parameter3Mode, false);
                    memory[multiplyOutputPosition] = multiply1 * multiply2;
                    position += 4;
                    break;
                case 3:
                    var save1 = Get(memory, position + 1, parameter1Mode, false);
                    memory[save1] = inputValue;
                    position += 2;
                    break;
                case 4:
                    outputValue = Get(memory, position + 1, parameter1Mode);
                    position += 2;
                    break;
                case 99:
                    return outputValue;
                default:
                    throw new NotImplementedException($"Opcode {opcode} at position {position}");
            }
        }
    }

    private static int Get(int[] memory, int position, int parameterMode, bool dereference = true)
    {
        var value = dereference ? memory[position] : position;

        // position mode
        if (parameterMode == 0)
            return memory[value];

        // immediate mode
        if (parameterMode == 1)
            return value;

        throw new NotImplementedException($"Parameter mode {parameterMode} not implemented");
    }
}
