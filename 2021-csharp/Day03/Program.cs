namespace Day03;

public static class Program
{
    // general apology: There is a LOT (A LOT) of duplicated code here. You have been warned.
    private static void Main()
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
            for (var i = 0; i < line.Length; i++)
                if (line[i] == '0')
                    zeroBits[i]++;
                else
                    oneBits[i]++;

        // apology: see above. I'm aware that leastCommonBits can be inferred from mostCommonBits,
        // and am choosing to double down on the duplication. 👍👍
        var mostCommonBits = new char[diagnosticReport[0].Length];
        var leastCommonBits = new char[diagnosticReport[0].Length];
        for (var i = 0; i < diagnosticReport[0].Length; i++)
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

        char[] mostCommonBits;
        char[] leastCommonBits;
        (mostCommonBits, _) = Get(diagnosticReport);
        var oxygenGeneratorRatingBits = new List<char>();
        var remaining = diagnosticReport;
        for (var i = 0; i < diagnosticReport[0].Length; i++)
        {
            var bit = mostCommonBits[i];
            oxygenGeneratorRatingBits.Add(bit);

            remaining = remaining.Where(x => x[i] == bit).ToArray();
            (mostCommonBits, _) = Get(remaining);
            if (remaining.Length == 1)
            {
                oxygenGeneratorRatingBits = remaining[0].ToList();
                break;
            }
        }

        (_, leastCommonBits) = Get(diagnosticReport);
        var co2ScrubberRatingBits = new List<char>();
        remaining = diagnosticReport;
        for (var i = 0; i < diagnosticReport[0].Length; i++)
        {
            var bit = leastCommonBits[i];
            co2ScrubberRatingBits.Add(bit);

            remaining = remaining.Where(x => x[i] == bit).ToArray();
            if (remaining.Length == 1)
            {
                co2ScrubberRatingBits = remaining[0].ToList();
                break;
            }

            (_, leastCommonBits) = Get(remaining);
        }

        var oxygenGeneratorRating = Convert.ToInt32(string.Join("", oxygenGeneratorRatingBits), 2);
        var co2ScrubberRating = Convert.ToInt32(string.Join("", co2ScrubberRatingBits), 2);

        return oxygenGeneratorRating * co2ScrubberRating;
    }

    private static (char[], char[]) Get(char[][] lines)
    {
        var length = lines[0].Length;

        var mostCommonBits = new char[length];
        var leastCommonBits = new char[length];
        // apology: I'm aware that oneBits can be inferred from zeroBits, and don't need to
        // be stored. But, it felt great and I'm honestly not apologizing.
        var zeroBits = new int[length];
        var oneBits = new int[length];

        foreach (var line in lines)
            for (var i = 0; i < line.Length; i++)
                if (line[i] == '0')
                    zeroBits[i]++;
                else
                    oneBits[i]++;

        // apology: see above. I'm aware that leastCommonBits can be inferred from mostCommonBits,
        // and am choosing to double down on the duplication. 👍👍
        for (var i = 0; i < length; i++)
        {
            mostCommonBits[i] = oneBits[i] >= zeroBits[i] ? '1' : '0';
            leastCommonBits[i] = zeroBits[i] <= oneBits[i] ? '0' : '1';
        }

        return (mostCommonBits, leastCommonBits);
    }
}