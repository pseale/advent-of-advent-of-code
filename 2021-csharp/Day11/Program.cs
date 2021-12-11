namespace Day11;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Total flashes after 100 steps: {partA}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var rows = lines.Length;
        var cols = lines[0].Length;

        var grid = new int[cols, rows];
        for (var row = 0; row < rows; row++)
        {
            var numbers = lines[row]
                .ToCharArray()
                .Select(x => int.Parse(x.ToString()))
                .ToArray();
            for (var col = 0; col < cols; col++) grid[col, row] = numbers[col];
        }

        var totalFlashes = 0;

        for (var step = 1; step <= 100; step++)
        {
            for (var col = 0; col < cols; col++)
            {
                for (var row = 0; row < rows; row++)
                {
                    grid[col, row]++;
                }
            }

            var newFlashes = new HashSet<(int col, int row)>(); // apology: this could be combined with the Queue below. But I won't 👍👍👍
            var queue = new Queue<(int col, int row)>();
            for (var col = 0; col < cols; col++)
            {
                for (var row = 0; row < rows; row++)
                {
                    if (grid[col, row] > 9)
                    {
                        newFlashes.Add((col, row));
                        queue.Enqueue((col, row));
                    }
                }
            }

            while (queue.Any())
            {
                var flash = queue.Dequeue();
                totalFlashes++;
                foreach (var neighbor in GetNeighbors(flash))
                {
                    grid[neighbor.col, neighbor.row]++;
                }

                for (var col = 0; col < cols; col++)
                {
                    for (var row = 0; row < rows; row++)
                    {
                        if (grid[col, row] > 9 && !newFlashes.Contains((col, row)))
                        {
                            newFlashes.Add((col, row));
                            queue.Enqueue((col, row));
                        }
                    }
                }
            }


            for (var col = 0; col < cols; col++)
            {
                for (var row = 0; row < rows; row++)
                {
                    if (grid[col, row] > 9)
                        grid[col, row] = 0;
                }
            }

            IEnumerable<(int col, int row)> GetNeighbors((int col, int row) point)
            {
                var directions = new (int col, int row)[]
                    {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
                return directions
                    .Select(x => (col: x.col + point.col, row: x.row + point.row))
                    .Where(direction => direction.col >= 0
                                               && direction.col < cols
                                               && direction.row >= 0
                                               && direction.row < rows);

            }
        }

        return totalFlashes;
    }
}