using System.Diagnostics;
using System.Text;

namespace Day13;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Visible dots after first fold: {partA}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var points = lines
            .Where(x => !x.Contains("fold along "))
            .Select(x => new Point(int.Parse(x.Split(",")[0]), int.Parse(x.Split(",")[1])))
            .ToArray();

        var rows = points.Max(x => x.Row + 1);
        var cols = points.Max(x => x.Col + 1);

        var grid = new bool[cols, rows];
        foreach (var point in points)
            grid[point.Col, point.Row] = true;

        var foldingInstructions = lines
            .Where(x => x.Contains("fold along "))
            .Select(x => ParseFoldingInstruction(x))
            .ToArray();

        var newGrid = ApplyFoldingInstruction(grid, foldingInstructions[0]);

        // ReSharper disable once ReplaceWithSingleCallToCount
        return GetPoints(newGrid)
            .Where(x => newGrid[x.Col, x.Row] == true)
            .Count();
    }

    private static void DebugPrintGrid(bool[,] grid)
    {
        var sb = new StringBuilder();
        var cols = grid.GetUpperBound(0);
        var rows = grid.GetUpperBound(1);
        for (int r = 0; r <= rows; r++)
        {
            for (int c = 0; c <= cols; c++)
            {
                if (grid[c, r])
                    sb.Append("#");
                else
                    sb.Append("·");
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }

    private static bool[,] ApplyFoldingInstruction(bool[,] grid, FoldingInstruction foldingInstruction)
    {
        // by example: folding on X
        // if grid = 500, x = 50, we fold 49 columns -> and return new grid with cols 51++
        var cols = grid.GetUpperBound(0) + 1;
        var rows = grid.GetUpperBound(1) + 1;

        if (foldingInstruction.Axis == FoldAxis.X)
        {
            var midpoint = cols / 2;
            if (midpoint != foldingInstruction.Value)
                throw new Exception("Only programmed this so it works with midpoints");

            for (int col = midpoint - 1; col >= 0; col--)
            {
                var colDistance = midpoint - col;
                for (int row = 0; row < rows; row++)
                {
                    grid[col, row] = grid[col, row] || grid[midpoint + colDistance, row];
                }
            }

            var newGrid = new bool[midpoint, rows];
            foreach (var point in GetPoints(newGrid))
                newGrid[point.Col, point.Row] = grid[point.Col, point.Row];
            return newGrid;
        }
        else
        {
            var midpoint = rows / 2;
            if (midpoint != foldingInstruction.Value)
                throw new Exception("Only programmed this so it works with midpoints");

            for (int row = midpoint - 1; row >= 0; row--)
            {
                var rowDistance = midpoint - row;
                for (int col = 0; col < cols; col++)
                {
                    grid[col, row] |= grid[col, midpoint + rowDistance];
                }
            }

            var newGrid = new bool[cols, midpoint];
            foreach (var point in GetPoints(newGrid))
                newGrid[point.Col, point.Row] = grid[point.Col, point.Row];
            return newGrid;
        }
    }

    // apology: this is icky. But I got the job done 👍
    private static IEnumerable<Point> GetPoints(bool[,] newGrid)
    {
        for (var col = 0; col <= newGrid.GetUpperBound(0); col++)
        {
            for (var row = 0; row <= newGrid.GetUpperBound(1); row++)
            {
                yield return new Point(col, row);
            }
        }
    }

    private static FoldingInstruction ParseFoldingInstruction(string x)
    {
        var words = x.Split(" ");
        var axis = words[2].Split("=")[0];
        var value = words[2].Split("=")[1];

        return new FoldingInstruction(
            Enum.Parse<FoldAxis>(axis.ToUpperInvariant()),
            int.Parse(value));
    }
}

public record Point(int Col, int Row);

public record FoldingInstruction(FoldAxis Axis, int Value);

public enum FoldAxis
{
    X,
    Y
}
