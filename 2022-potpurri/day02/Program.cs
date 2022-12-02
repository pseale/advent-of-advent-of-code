

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
        .Select(x => new RoundPartA { Them = Parse(x.Split(" ")[0]), Us = Parse(x.Split(" ")[1]) });
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
long SolvePartB(string[] input)
{
    var rounds = input.Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(x => new RoundPartB { Them = Parse(x.Split(" ")[0]), WhatShouldWeDo = ParseWinLoseDraw(x.Split(" ")[1]) })
        .ToArray();

    long score = 0;
    foreach (var round in rounds)
    {
        if (round.WhatShouldWeDo == WinLoseDraw.Draw)
        {
            score += 3;
            // pick what they pick
            score += (long)round.Them;
        } else if (round.WhatShouldWeDo == WinLoseDraw.Win)
        {
            score += 6;
            if (round.Them == RPS.Rock) score += (long)RPS.Paper;
            else if (round.Them == RPS.Paper) score += (long)RPS.Scissors;
            else if (round.Them == RPS.Scissors) score += (long)RPS.Rock;
            else throw new NotImplementedException();
        } else
        {

            score += 0;
            if (round.Them == RPS.Rock) score += (long)RPS.Scissors;
            else if (round.Them == RPS.Paper) score += (long)RPS.Rock;
            else if (round.Them == RPS.Scissors) score += (long)RPS.Paper;
            else throw new NotImplementedException();
        }
    }

    return score;

}

WinLoseDraw ParseWinLoseDraw(string v)
{
    if (v == "X") return WinLoseDraw.Lose;
    if (v == "Y") return WinLoseDraw.Draw;
    if (v == "Z") return WinLoseDraw.Win;
    throw new NotImplementedException();
}

var exampleInput = new[] { "A Y", "B X", "C Z" };
var examplePartA = SolvePartA(exampleInput);
var examplePartB = SolvePartB(exampleInput);


Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected 15)");

var partA = SolvePartA(input);
var partB = SolvePartB(input);

Console.WriteLine($"Part A: {partA}");

Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected 12)");
Console.WriteLine($"Part B: {partB}");