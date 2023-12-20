var sampleInput = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";
SolvePart1(sampleInput, 13);
SolvePart1(File.ReadAllText("input.txt"));


void SolvePart1(string input, int? expected = null)
{
    var lines = input.Split("\n").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    var visited = new HashSet<string>();
    var headColumn = 0;
    var headRow = 0;
    var tailColumn = 0;
    var tailRow = 0;

    var lastDirection = (0, 0);
    foreach (var line in lines)
    {
        var words = line.Split(" ");
        var direction = GetDirection(words[0]);
        var magnitude = int.Parse(words[1]);
        for (int i = 0; i < magnitude; i++)
        {
            headColumn += direction.column;
            headRow += direction.row;

            if (Math.Abs(headColumn - tailColumn) > 1)
            {
                tailColumn = int.Clamp(tailColumn, headColumn - 1, headColumn + 1);
                tailRow = headRow;
            } else if (Math.Abs(headRow - tailRow) > 1)
            {
                tailColumn = headColumn;
                tailRow = int.Clamp(tailRow, headRow - 1, headRow + 1);
            }
            else
            {
                tailColumn = int.Clamp(tailColumn, headColumn - 1, headColumn + 1);
                tailRow = int.Clamp(tailRow, headRow - 1, headRow + 1);
            }

            visited.Add($"({tailColumn}, {tailRow})");
        }
        
    }
    var answer = visited.Count;
    string expectedString = expected != null ? $" - Expected: {expected.Value}" : "";
    foreach (var s in visited.ToList().OrderBy(x => x))
    {
        if (expected.HasValue)
            Console.WriteLine(s);
    }
    Console.WriteLine($"Part 1 Answer: {answer}{expectedString}");
}

(int column, int row) GetDirection(string command)
{
    if (command == "U")
        return (0, 1);
    else if (command == "D")
        return (0, -1);
    else if (command == "L")
        return (-1, 0);
    else if (command == "R")
        return (1, 0);
    else throw new Exception($"Invalid command: got {command}");
}