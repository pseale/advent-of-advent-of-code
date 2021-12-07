
namespace Day05;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Points where at least two lines overlap: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Points where at least two lines overlap (including diagonal): {partB}");
    }

    public static int SolvePartA(string input)
    {
        var l = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var vents = new List<Vent>();
        foreach (var line in l)
        {
            var words = line.Split(" ");
            // 0,9 -> 5,9
            var col1 = int.Parse(words[0].Split(",")[0]);
            var row1 = int.Parse(words[0].Split(",")[1]);

            var col2 = int.Parse(words[2].Split(",")[0]);
            var row2 = int.Parse(words[2].Split(",")[1]);
            vents.Add(new Vent(col1, row1, col2, row2));
        }


        var verticalOrHorizontal = vents
            .Where(x => x.Col1 == x.Col2 || x.Row1 == x.Row2)
            .ToList();

        var maxCol = vents.Max(x => Math.Max(x.Col1, x.Col2)) + 2;
        var maxRow = vents.Max(x => Math.Max(x.Row1, x.Row2)) + 2;

        var points = new int[maxCol, maxRow];

        foreach (var vent in verticalOrHorizontal)
        {
            foreach (var point in PartAGetPoints(vent))
                points[point.Col, point.Row] += 1;
        }

        var hotspots = 0;
        for (int col = 0; col < maxCol; col++)
            for (int row = 0; row < maxRow; row++)
                if (points[col, row] >= 2)
                    hotspots++;

        return hotspots;
    }

    // apology: I am ashamed of this. It works though 👍
    private static List<Point> PartAGetPoints(Vent vent)
    {
        var leftRightDirection = vent.Col2 - vent.Col1 > 0
            ? 1
            : vent.Col2 - vent.Col1 < 0 ? -1 : 0;

        var upDownDirection = vent.Row2 - vent.Row1 > 0
            ? 1
            : vent.Row2 - vent.Row1 < 0 ? -1 : 0;

        var distance = Math.Abs((vent.Col2 - vent.Col1) + (vent.Row2 - vent.Row1));

        // 9,4 -> 3,4      9,4 8,4 7,4 6,4 5,4 4,4 3,4
        //                 i=0 i=1 i=2 i=3 i=4 i=5 i=6
        var points = new List<Point>();
        for (int i = 0; i <= distance; i++)
        {
            points.Add(new Point(vent.Col1 + i * leftRightDirection, vent.Row1 + i * upDownDirection));
        }

        return points;
    }

    public static int SolvePartB(string input)
    {
        var l = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var vents = new List<Vent>();
        foreach (var line in l)
        {
            var words = line.Split(" ");
            // 0,9 -> 5,9
            var col1 = int.Parse(words[0].Split(",")[0]);
            var row1 = int.Parse(words[0].Split(",")[1]);

            var col2 = int.Parse(words[2].Split(",")[0]);
            var row2 = int.Parse(words[2].Split(",")[1]);
            vents.Add(new Vent(col1, row1, col2, row2));
        }


        var maxCol = vents.Max(x => Math.Max(x.Col1, x.Col2)) + 2;
        var maxRow = vents.Max(x => Math.Max(x.Row1, x.Row2)) + 2;

        var points = new int[maxCol, maxRow];

        foreach (var vent in vents)
        {
            foreach (var point in PartBGetPoints(vent))
                points[point.Col, point.Row] += 1;
        }

        var hotspots = 0;
        for (int col = 0; col < maxCol; col++)
            for (int row = 0; row < maxRow; row++)
                if (points[col, row] >= 2)
                    hotspots++;

        return hotspots;
    }

    // retraction: no longer ashamed of the apology comment above. Live out loud 👍👍
    private static List<Point> PartBGetPoints(Vent vent)
    {
        var leftRightDistance = (vent.Col2 - vent.Col1);
        var upDownDistance = vent.Row2 - vent.Row1;

        var leftRightDirection = leftRightDistance > 0
            ? 1
            : leftRightDistance < 0 ? -1 : 0;

        var upDownDirection = upDownDistance > 0
            ? 1
            : upDownDistance < 0 ? -1 : 0;

        var distance = Math.Max(Math.Abs(leftRightDistance), Math.Abs(upDownDistance));

        // 9,4 -> 3,4      9,4 8,4 7,4 6,4 5,4 4,4 3,4
        //                 i=0 i=1 i=2 i=3 i=4 i=5 i=6
        var points = new List<Point>();
        for (int i = 0; i <= distance; i++)
        {
            points.Add(new Point(vent.Col1 + i * leftRightDirection, vent.Row1 + i * upDownDirection));
        }

        return points;
    }

    public record Vent(int Col1, int Row1, int Col2, int Row2);

    public record Point(int Col, int Row);
}
