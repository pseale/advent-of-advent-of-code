var exampleInput = await File.ReadAllLinesAsync(@".\example.txt");

static long Score(char c)
{
    if (c >= 'a' && c <= 'z') return 0 + 1 + ((long)c - (long)'a');
    else if (c >= 'A' && c <= 'Z') return 26 + 1 + ((long)c - (long)'A');
    throw new NotImplementedException();
}

var input = await File.ReadAllLinesAsync(@".\input.txt");

long SolvePartA(string[] input)
{
    long score = 0;
    foreach (var line in input)
    {
        var leftHalf = line.Substring(0, line.Length / 2);
        var rightHalf = line.Substring(line.Length / 2);
        char c = FindSameCharIn(leftHalf, rightHalf);

        score += Score(c);
    }
    return score;
}

char FindSameCharIn(string leftHalf, string rightHalf)
{
    var dictionary = new Dictionary<char, int>();

    leftHalf.ToCharArray().Distinct().ToList().ForEach(x => {
        if (!dictionary.TryAdd(x, 1)) dictionary[x] += 1;
    });
    rightHalf.ToCharArray().Distinct().ToList().ForEach(x => {
        if (!dictionary.TryAdd(x, 1)) dictionary[x] += 1;
    });
    return dictionary.Single(x => x.Value == 2).Key;
}

long SolvePartB(string[] input)
{
    return 0;
}


var examplePartA = SolvePartA(exampleInput);
var partA = SolvePartA(input);

Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected 157)");
Console.WriteLine($"Part A: {partA}");

var examplePartB = SolvePartB(exampleInput);
var partB = SolvePartB(input);
Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected TODO)");
Console.WriteLine($"Part B: {partB}");

