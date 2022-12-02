

using Day02;

var input = await File.ReadAllLinesAsync(@".\input.txt");

RPS Parse(string v)
{
    if (v == "A" || v == "X")
        return RPS.Rock;
    if (v == "B" || v == "Y")
        return RPS.Paper;
    if (v == "C" || v == "Z")
        return RPS.Scissors;

    throw new NotImplementedException();
}

long SolvePartA(string[] input)
{
    var rounds = input.Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(x => new Round { Them = Parse(x.Split(" ")[0]), Us = Parse(x.Split(" ")[1]) });
    long score = 0;
    foreach (var round in rounds)
    {
        score += (long)round.Us;
        if (round.Us == round.Them)
        {
            score += 3;
        }
        else if (round.Us == RPS.Rock && round.Them == RPS.Scissors
            || round.Us == RPS.Paper && round.Them == RPS.Rock
            || round.Us == RPS.Scissors && round.Them == RPS.Paper)
        {
            score += 6;
        }
        else
        {
            score += 0;
        }
    }

    return score;
}

var exampleInput = new[] { "A Y", "B X", "C Z" };
var examplePartA = SolvePartA(exampleInput);


Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected 15)");

var partA = SolvePartA(input);
var partB = "";
Console.WriteLine($"Part A: {partA}");

Console.WriteLine($"Part B: {partB}");