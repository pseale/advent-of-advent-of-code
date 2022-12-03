var exampleInput = await File.ReadAllLinesAsync(@".\example.txt");
var input = await File.ReadAllLinesAsync(@".\input.txt");

var examplePartA = SolvePartA(exampleInput);
var partA = SolvePartA(input);

Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected 157)");
Console.WriteLine($"Part A: {partA}");

var examplePartB = SolvePartB(exampleInput);
var partB = SolvePartB(input);
Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected 70)");
Console.WriteLine($"Part B: {partB}");

static long Score(char c)
{
    if (c >= 'a' && c <= 'z') return 0 + 1 + ((long)c - (long)'a');
    else if (c >= 'A' && c <= 'Z') return 26 + 1 + ((long)c - (long)'A');
    throw new Exception("Expected 'a-z' or 'A-Z'; got " + c);
}

long SolvePartA(string[] input)
{
    long score = 0;
    foreach (var line in input)
    {
        var leftHalf = line.Substring(0, line.Length / 2);
        var rightHalf = line.Substring(line.Length / 2);
        char c = FindSameItemInAll(new[] { leftHalf, rightHalf });
        score += Score(c);
    }
    return score;
}

long SolvePartB(string[] input)
{
    long score = 0;
    var groupsOfThree = input.Chunk(3).ToArray();
    foreach (var group in groupsOfThree)
    {
        char c = FindSameItemInAll(group);
        score += Score(c);
    }
    return score;
}

char FindSameItemInAll(string[] rucksacks)
{
    var dictionary = new Dictionary<char, int>();

    foreach (var rucksack in rucksacks)
    {
        rucksack.ToCharArray().Distinct().ToList().ForEach(item => {
            if (!dictionary.TryAdd(item, 1)) dictionary[item] += 1;
        });
    }
    return dictionary.Single(x => x.Value == rucksacks.Length).Key;
}
