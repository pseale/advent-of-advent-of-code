namespace Day09;

public static class Program
{
    private static int _rows;
    private static int _cols;

    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Sum of the risk levels of all low points: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Three largest basins (multiplied): {partB}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        _rows = lines.Length;
        _cols = lines[0].Length;

        var grid = new int[_cols, _rows];
        for (var row = 0; row < _rows; row++)
        {
            var numbers = lines[row]
                .ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            for (var col = 0; col < _cols; col++) grid[col, row] = numbers[col];
        }

        var totalRiskLevel = 0;
        for (var row = 0; row < _rows; row++)
        for (var col = 0; col < _cols; col++)
            if (IsLowPoint(grid, col, row))
                totalRiskLevel += 1 + grid[col, row];
        return totalRiskLevel;
    }

    public static int SolvePartB(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        _rows = lines.Length;
        _cols = lines[0].Length;

        var grid = new int[_cols, _rows];
        for (var row = 0; row < _rows; row++)
        {
            var numbers = lines[row]
                .ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            for (var col = 0; col < _cols; col++) grid[col, row] = numbers[col];
        }

        var lowPoints = new List<(int col, int row)>();
        for (var row = 0; row < _rows; row++)
        for (var col = 0; col < _cols; col++)
            if (IsLowPoint(grid, col, row))
                lowPoints.Add((col, row));

        var basins = new List<(int col, int row)[]>();
        foreach (var lowPoint in lowPoints) basins.Add(GetBasinFor(grid, lowPoint));

        var top3 = basins.OrderByDescending(x => x.Length)
            .Take(3)
            .ToArray();

        return top3[0].Length * top3[1].Length * top3[2].Length;
    }

    private static (int col, int row)[] GetBasinFor(int[,] grid, (int col, int row) lowPoint)
    {
        var basin = new List<(int col, int row)>();

        var queue = new Queue<(int col, int row)>();
        queue.Enqueue(lowPoint);

        while (queue.Any())
        {
            var point = queue.Dequeue();
            if (basin.Contains(point))
                continue;

            var depth = grid[point.col, point.row];
            // check all 4 directions
            var adjacentIsLower = false;
            foreach (var adjacent in GetAdjacentPoints(grid, point))
            {
                if (basin.Contains(adjacent)) continue;

                var adjacentPointDepth = grid[adjacent.col, adjacent.row];
                if (adjacentPointDepth == 9) continue;

                if (adjacentPointDepth < depth)
                {
                    adjacentIsLower = true;
                    continue;
                }

                if (adjacentPointDepth == depth) continue;

                queue.Enqueue(adjacent);
            }

            //  - if ALL ADJACENT points are higher, add it to the basin AND "check all 4 directions" (recurse)
            if (!adjacentIsLower) basin.Add(point);
        }

        return basin.ToArray();
    }

    private static IEnumerable<(int col, int row)> GetAdjacentPoints(int[,] grid, (int col, int row) point)
    {
        var adjacentPoints = new List<(int col, int row)>();

        if (IsWithinGrid(grid, point.col + 1, point.row)) adjacentPoints.Add((point.col + 1, point.row));
        if (IsWithinGrid(grid, point.col - 1, point.row)) adjacentPoints.Add((point.col - 1, point.row));
        if (IsWithinGrid(grid, point.col, point.row + 1)) adjacentPoints.Add((point.col, point.row + 1));
        if (IsWithinGrid(grid, point.col, point.row - 1)) adjacentPoints.Add((point.col, point.row - 1));

        return adjacentPoints;
    }

    private static bool IsLowPoint(int[,] grid, int col, int row)
    {
        var height = grid[col, row];

        return IsHigherAndWithinGrid(grid, col + 1, row, height)
               && IsHigherAndWithinGrid(grid, col - 1, row, height)
               && IsHigherAndWithinGrid(grid, col, row + 1, height)
               && IsHigherAndWithinGrid(grid, col, row - 1, height);
    }

    // Returns true if the point is higher, or returns true if the point doesn't exist on the grid
    private static bool IsHigherAndWithinGrid(int[,] grid, int col, int row, int height)
    {
        if (!IsWithinGrid(grid, col, row))
            return true;

        return grid[col, row] > height;
    }

    private static bool IsWithinGrid(int[,] grid, int col, int row)
    {
        if (col < 0 || col >= _cols) return false;
        if (row < 0 || row >= _rows) return false;

        return true;
    }
}