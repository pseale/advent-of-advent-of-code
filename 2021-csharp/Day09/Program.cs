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
        for (int row = 0; row < _rows; row++)
        {
            var numbers = lines[row]
                .ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            for (int col = 0; col < _cols; col++)
            {
                grid[col, row] = numbers[col];
            }
        }

        var totalRiskLevel = 0;
        for (int row = 0; row < _rows; row++)
        for (int col = 0; col < _cols; col++)
        {
            if (IsLowPoint(grid, col, row))
                totalRiskLevel += 1 + grid[col, row];
        }
        return totalRiskLevel;
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
        if (col < 0 || col >= _cols)
            return true;
        if (row < 0 || row >= _rows)
            return true;

        return grid[col, row] > height;
    }
}
