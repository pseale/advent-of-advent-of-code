var exampleInput = await File.ReadAllLinesAsync(@".\example.txt");
var input = await File.ReadAllLinesAsync(@".\input.txt");

long SolvePartA(string[] input)
{
    throw new NotImplementedException();
}
long SolvePartB(string[] input)
{
    throw new NotImplementedException();
}


var examplePartA = SolvePartA(exampleInput);
var partA = SolvePartA(input);
var examplePartB = SolvePartB(exampleInput);
var partB = SolvePartB(input);

Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected TODO)");
Console.WriteLine($"Part A: {partA}");
Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected TODO)");
Console.WriteLine($"Part B: {partB}");

