using System.Text;
// ReSharper disable ReplaceWithSingleCallToFirst
// ReSharper disable ReplaceWithSingleCallToCount
// ReSharper disable ReplaceWithSingleCallToSingle
// ReSharper disable PossibleMultipleEnumeration

namespace Day08;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"digits 1,4,7,8 appear this many times in the outputValue: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Sum of all output values: {partB}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var entries = lines
            .Select(x => x.Split(" | ")[1].Split(" "))
            .ToArray();

        var count = 0;
        foreach (var entry in entries)
        {
            count += entry.Where(x => x.Length is 2 or 3 or 4 or 7).Count();
        }
        return count;
    }

    public static int SolvePartB(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();


        var signalPatterns = lines
            .Select(x => x.Split(" | ")[0].Split(" "))
            .ToArray();

        var outputValueStrings = lines
            .Select(x => x.Split(" | ")[1].Split(" "))
            .ToArray();


        var count = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var decoded = Decode(signalPatterns[i]);
            count += GetOutputValue(outputValueStrings[i], decoded);
        }

        return count;
    }

    private static Dictionary<char, Segment> Decode(string[] signalPattern)
    {
        var stuff = signalPattern
            .Select(x => x.ToCharArray())
            .ToArray();

        var allSegments = stuff.SelectMany(x => x).ToArray();
        var segmentFrequency = GetSegmentFrequency(allSegments);

        var segments = new Dictionary<char, Segment>();

        var seven = stuff.Where(x => x.Length == 3).Single();
        var one = stuff.Where(x => x.Length == 2).Single();
        var four = stuff.Where(x => x.Length == 4).Single();

        // a segment => contained by 7, but not by 1
        var a = seven.Except(one).Single();
        segments[a] = Segment.Top;

        // b segment => used by 5 glyphs total
        var b = segmentFrequency
            .Where(x => x.Value == 6)
            .Single()
            .Key;
        segments[b] = Segment.TopLeft;

        // c segment => the other segment, not a, used by 8 glyphs total
        var c = segmentFrequency
            .Where(x => x.Value == 8)
            .Where(x => x.Key != a)
            .First()
            .Key;
        segments[c] = Segment.TopRight;

        // d segment => used by 7 glyphs total, INCLUDING '4'
        var d = segmentFrequency
            .Where(x => x.Value == 7)
            .Where(x => four.Contains(x.Key))
            .First()
            .Key;
        segments[d] = Segment.Middle;

        // e segment => used by 4 glyphs total
        var e = segmentFrequency
            .Where(x => x.Value == 4)
            .First()
            .Key;
        segments[e] = Segment.BottomLeft;

        // f segment => used by 9 glyphs total
        var f = segmentFrequency
            .Where(x => x.Value == 9)
            .First()
            .Key;
        segments[f] = Segment.BottomRight;

        // g segment => used by 7 glyphs total, NOT INCLUDING '4'
        var g = segmentFrequency
            .Where(x => x.Value == 7)
            .Where(x => !four.Contains(x.Key))
            .First()
            .Key;
        segments[g] = Segment.Bottom;

        return segments;
    }

    // apology: I feel bad
    private static Dictionary<char, int> GetSegmentFrequency(char[] allSegments)
    {
        var f = new Dictionary<char, int>();
        foreach (var c in allSegments)
        {
            if (f.ContainsKey(c))
                f[c] += 1;
            else
                f[c] = 1;
        }

        return f;
    }

    private static int GetOutputValue(string[] outputValueStrings, Dictionary<char, Segment> decoded)
    {
        var n = GetNumberSegments();
        var digits = "";
        foreach (var outputValueString in outputValueStrings)
        {
            var chars = outputValueString.ToCharArray();
            var segments = chars.Select(x => decoded[x]).ToArray();
            var digit = n
                .Where(x => x.Value.Count == segments.Length)
                .Where(x => x.Value.All(xValue => segments.Contains(xValue)))
                .Single()
                .Key
                .ToString();
            digits += digit;
        }
        return int.Parse(digits);
    }

    private static Dictionary<int, List<Segment>> GetNumberSegments()
    {
        var ns = new Dictionary<int, List<Segment>>();

        ns.Add(0, new List<Segment>() {Segment.TopLeft, Segment.Top, Segment.TopRight, Segment.BottomLeft, Segment.Bottom, Segment.BottomRight});
        ns.Add(1, new List<Segment>() {Segment.TopRight, Segment.BottomRight});
        ns.Add(2, new List<Segment>() {Segment.Top, Segment.TopRight, Segment.Middle, Segment.BottomLeft, Segment.Bottom});
        ns.Add(3, new List<Segment>() {Segment.Top, Segment.TopRight, Segment.Middle, Segment.BottomRight, Segment.Bottom});
        ns.Add(4, new List<Segment>() {Segment.TopLeft, Segment.TopRight, Segment.Middle, Segment.BottomRight});
        ns.Add(5, new List<Segment>() {Segment.TopLeft, Segment.Top, Segment.Middle, Segment.BottomRight, Segment.Bottom});
        ns.Add(6, new List<Segment>() {Segment.TopLeft, Segment.Top, Segment.Middle, Segment.BottomLeft, Segment.Bottom, Segment.BottomRight});
        ns.Add(7, new List<Segment>() {Segment.Top, Segment.TopRight, Segment.BottomRight});
        ns.Add(8, new List<Segment>() {Segment.TopLeft, Segment.Top, Segment.TopRight, Segment.Middle, Segment.BottomLeft, Segment.Bottom, Segment.BottomRight});
        ns.Add(9, new List<Segment>() {Segment.TopLeft, Segment.Top, Segment.TopRight, Segment.Middle, Segment.Bottom, Segment.BottomRight});

        return ns;
    }

    public enum Segment
    {
        Top,
        TopLeft,
        TopRight,
        Middle,
        BottomLeft,
        Bottom,
        BottomRight
    }
}