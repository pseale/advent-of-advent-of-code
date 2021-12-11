namespace Day11;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var (partA, partB) = Solve(input);
        Console.WriteLine($"Total flashes after 100 steps: {partA}");
        Console.WriteLine($"First step during which all octopuses flash: {partB}");
    }

    public static (int, int) Solve(string input)
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

        var step = 1;
        while (true)
        {
            foreach (var point in GetPoints())
            {
                grid[point.col, point.row]++;
            }

            var newFlashes = new HashSet<(int col, int row)>(); // apology: this could be combined with the Queue below. But I won't 👍👍👍
            var queue = new Queue<(int col, int row)>();
            foreach (var point in GetPoints())
            {
                if (grid[point.col, point.row] > 9)
                {
                    newFlashes.Add((point.col, point.row));
                    queue.Enqueue((point.col, point.row));
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

                foreach (var point in GetPoints())
                {
                    if (grid[point.col, point.row] > 9 && !newFlashes.Contains((point.col, point.row)))
                    {
                        newFlashes.Add((point.col, point.row));
                        queue.Enqueue((point.col, point.row));
                    }
                }
            }


            foreach (var point in GetPoints())
            {
                if (grid[point.col, point.row] > 9) grid[point.col, point.row] = 0;
            }

            IEnumerable<(int col, int row)> GetPoints()
            {
                for (var col = 0; col < cols; col++)
                {
                    for (var row = 0; row < rows; row++)
                    {
                        yield return (col, row);
                    }
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

            if (step == 100)
                return (totalFlashes, -1);
            step++;
        }
    }
}