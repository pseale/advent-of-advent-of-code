using System.Text.RegularExpressions;

namespace Day12;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = Solve(input, false);
        Console.WriteLine($"Paths: {partA}");

        var partB = Solve(input, true);
        Console.WriteLine($"Paths (visiting a single small cave twice): {partB}");
    }

    public static int Solve(string input, bool allowDoubleDipping)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var edges = lines
            .Select(x => new Edge(x.Split("-")[0], x.Split("-")[1]))
            .ToArray();

        var paths = new List<List<string>>();

        var queue = new Queue<List<string>>();
        queue.Enqueue(new List<string>() {"start"});

        while (queue.Any())
        {
            var visited = queue.Dequeue();
            // push all these into the queue THAT ARE VALID
            var allAdjacent = GetAdjacent(visited);
            foreach (var adjacent in allAdjacent)
            {
                // if we go from start->end, this is a valid 'path', thus store it in 'paths'
                if (adjacent == "end")
                    paths.Add(visited.Append("end").ToList());
                else if (IsValid(visited, adjacent, allowDoubleDipping))
                    queue.Enqueue(visited.Append(adjacent).ToList());
            }
        }

        return paths.Count;

        IEnumerable<string> GetAdjacent(List<string> visited)
        {
            foreach (var edge in edges)
            {
                if (edge.Start == visited[^1])
                    yield return edge.End;
                if (edge.End == visited[^1])
                    yield return edge.Start;
            }
        }

        bool IsValid(List<string> visited, string adjacent, bool allowDoubleDipping)
        {
            if (adjacent == "start")
                return false;

            bool wouldCauseDoubleDipping = IsSmallCave(adjacent) && visited.Contains(adjacent);

            if (!allowDoubleDipping)
            {
                return !wouldCauseDoubleDipping;
            }

            bool hasAlreadyDoubleDipped = visited
                .Where(x => IsSmallCave(x))
                .GroupBy(x => x)
                .Any(x => x.Count() > 1);

            if (hasAlreadyDoubleDipped && wouldCauseDoubleDipping)
                return false;

            return true;
        }
    }

    private static bool IsSmallCave(string adjacent)
    {
        return Regex.IsMatch(adjacent, "^[a-z]+$");
    }
}

public record Edge(string Start, string End);
