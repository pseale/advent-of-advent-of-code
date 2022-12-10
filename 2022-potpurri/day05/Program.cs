using System.Text.RegularExpressions;

var exampleInput = await File.ReadAllTextAsync(@".\example.txt");
var input = await File.ReadAllTextAsync(@".\input.txt");

var examplePartA = SolvePartA(exampleInput);
var partA = SolvePartA(input);

Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected CMZ)");
Console.WriteLine($"Part A: {partA}");

var examplePartB = SolvePartB(exampleInput);
var partB = SolvePartB(input);
Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected TBD)");
Console.WriteLine($"Part B: {partB}");

string SolvePartA(string input)
{
    var (stacks, procedures) = Parse(input);
    foreach (var procedure in procedures)
    {
        var fromStack = stacks[procedure.from];
        var toStack = stacks[procedure.to];
        for (int i = 0; i < procedure.quantity; i++)
        {
            var crate = fromStack.Pop();
            toStack.Push(crate);
        }
    }

    var topCrate = "";
    foreach (var stack in stacks)
    {
        topCrate += stack.Value.Pop();
    }
    return topCrate;
}

(Dictionary<int, Stack<char>> stacks, List<Procedure> procedures) Parse(string input)
{
    var normalizedInput = input.Replace("\r", ""); // I don't want to deal with Windows line endings, just 'normalize' into LF-only line endings 🤷‍
    var stacks = ParseStacks(normalizedInput.Split("\n\n")[0].Split("\n"));

    var procedures = ParseProcedures(normalizedInput.Split("\n\n")[1].Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
    return (stacks, procedures);
}

Dictionary<int, Stack<char>> ParseStacks(string[] lines)
{
    var stacks = new Dictionary<int, Stack<char>>();
    var trimmedLine = lines[^1].Trim();
    var binStrings = Regex.Split(trimmedLine, @"\s+");
    var bins = binStrings.Select(x => int.Parse(x));
    foreach (var bin in bins)
    {
        stacks.Add(bin, new Stack<char>());
    }

    // '[Q] [J]                         [H]'
    // ' 1   2   3   4   5   6   7   8   9 '
    // Q is character offset 1, bin offset 0
    // J is character offset 5, bin offset 1
    // H is character offset 33, bin offset 8
    // thus the formula is: bin # - 1 = bin offset. bin offset * 4 + 1 is the character offset
    var contents = lines.Take(lines.Length - 1).ToList();
    for (int i = contents.Count - 1; i >= 0; i--)
    {
        var line = contents[i];
        foreach (var bin in stacks.Keys)
        {
            var binOffset = bin - 1;
            var charLocation = binOffset * 4 + 1;
            var @char = line[charLocation];
            if (@char != ' ')
                stacks[bin].Push(@char);
        }
    }

    return stacks;
}

List<Procedure> ParseProcedures(string[] lines)
{
    var list = new List<Procedure>();
    foreach (var line in lines)
    {
        var words = line.Split(" ");
        // example line:
        // move 1 from 2 to 1
        var quantity = int.Parse(words[1]);
        var from = int.Parse(words[3]);
        var to = int.Parse(words[5]);

        list.Add(new Procedure(quantity, from, to));
    }
    return list;
}

string SolvePartB(string input)
{
    return "";
}

record Procedure(int quantity, int from, int to);
