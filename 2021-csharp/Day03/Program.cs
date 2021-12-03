namespace Day03;

public static class Program
{
    static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Power consumption of the submarine: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Life support rating of the submarine: {partB}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var diagnosticReport = lines
            .Select(x => x.ToCharArray())
            .ToArray();

        // apology: I'm aware that oneBits can be inferred from zeroBits, and don't need to
        // be stored. But, it felt great and I'm honestly not apologizing.
        var zeroBits = new int[diagnosticReport[0].Length];
        var oneBits = new int[diagnosticReport[0].Length];

        foreach (var line in diagnosticReport)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '0')
                    zeroBits[i]++;
                else
                    oneBits[i]++;
            }
        }

        // apology: see above. I'm aware that leastCommonBits can be inferred from mostCommonBits,
        // and am choosing to double down on the duplication. 👍👍
        var mostCommonBits = new char[diagnosticReport[0].Length];
        var leastCommonBits = new char[diagnosticReport[0].Length];
        for (int i = 0; i < diagnosticReport[0].Length; i++)
        {
            mostCommonBits[i] = oneBits[i] > zeroBits[i] ? '1' : '0';
            leastCommonBits[i] = oneBits[i] > zeroBits[i] ? '0' : '1';
        }

        // apology: see above. Tripling down on storing values that could be inferred instead. 👍👍👍
        var gammaRate = Convert.ToInt32(string.Join("", mostCommonBits), 2);
        var epsilonRate = Convert.ToInt32(string.Join("", leastCommonBits), 2);

        return gammaRate * epsilonRate;
    }

    public static int SolvePartB(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var diagnosticReport = lines
            .Select(x => x.ToCharArray())
            .ToArray();

        // apology: I'm aware that oneBits can be inferred from zeroBits, and don't need to
        // be stored. But, it felt great and I'm honestly not apologizing.
        var zeroBits = new int[diagnosticReport[0].Length];
        var oneBits = new int[diagnosticReport[0].Length];

        foreach (var line in diagnosticReport)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '0')
                    zeroBits[i]++;
                else
                    oneBits[i]++;
            }
        }

        // apology: see above. I'm aware that leastCommonBits can be inferred from mostCommonBits,
        // and am choosing to double down on the duplication. 👍👍
        var mostCommonBits = new char[diagnosticReport[0].Length];
        var leastCommonBits = new char[diagnosticReport[0].Length];
        for (int i = 0; i < diagnosticReport[0].Length; i++)
        {
            mostCommonBits[i] = oneBits[i] > zeroBits[i] ? '1' : '0';
            leastCommonBits[i] = oneBits[i] > zeroBits[i] ? '0' : '1';
        }

        return -1;
    }
}
