var exampleInput = await File.ReadAllLinesAsync(@".\example.txt");
var input = await File.ReadAllLinesAsync(@".\input.txt");

var examplePartA = SolvePartA(exampleInput);
var partA = SolvePartA(input);

Console.WriteLine($"EXAMPLE Part A: {examplePartA} (expected 2)");
Console.WriteLine($"Part A: {partA}");

var examplePartB = SolvePartB(exampleInput);
var partB = SolvePartB(input);
Console.WriteLine($"EXAMPLE Part B: {examplePartB} (expected 4)");
Console.WriteLine($"Part B: {partB}");

long SolvePartA(string[] input)
{
    var score = 0;
    foreach (var line in input)
    {
        var split = line.Split(",");
        var nums1 = split[0].Split("-");
        var nums2 = split[1].Split("-");
        var a = new Assignment(long.Parse(nums1[0]), long.Parse(nums1[1]));
        var b = new Assignment(long.Parse(nums2[0]), long.Parse(nums2[1]));

        if (FullyOverlaps(a, b))
            score++;
    }

    return score;
}

bool FullyOverlaps(Assignment a, Assignment b)
{
    return AContainsB(a, b) || AContainsB(b, a);
}

bool AContainsB(Assignment a, Assignment b)
{
    return a.minSectionId <= b.minSectionId && a.maxSectionId >= b.maxSectionId;
}

long SolvePartB(string[] input)
{
    var score = 0;
    foreach (var line in input)
    {
        var split = line.Split(",");
        var nums1 = split[0].Split("-");
        var nums2 = split[1].Split("-");
        var a = new Assignment(long.Parse(nums1[0]), long.Parse(nums1[1]));
        var b = new Assignment(long.Parse(nums2[0]), long.Parse(nums2[1]));

        if (OverlapsAtAll(a, b))
            score++;
    }

    return score;
}

bool OverlapsAtAll(Assignment a, Assignment b)
{
    if (a.minSectionId <= b.minSectionId && a.maxSectionId >= b.minSectionId)
        return true;

    if (b.minSectionId <= a.minSectionId && b.maxSectionId >= a.minSectionId)
        return true;

    if (a.maxSectionId >= b.maxSectionId && a.minSectionId <= b.maxSectionId) 
        return true;

    if (b.maxSectionId >= a.maxSectionId && b.minSectionId <= a.maxSectionId) 
        return true;

    return false;
}

record Assignment(long minSectionId, long maxSectionId);
