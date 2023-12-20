using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");

var sampleInput = @"30373
25512
65332
33549
35390";
SolvePart1(sampleInput, 21);
SolvePart1(File.ReadAllText("input.txt"));

void SolvePart1(string input, int? expected = null)
{
    var lines = input.Split("\n").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    var columns = lines[0].Length;
    var rows = lines.Count;
    var trees = new int[columns, rows];
    for (int row = 0; row < lines.Count; row++)
    {
        for (int column = 0; column < lines[row].Length; column++)
        {
            trees[column, row] = int.Parse(lines[row][column].ToString());
        }
    }
    var answer = 0;

    for (int row = 0; row < lines.Count; row++)
    {
        for (int column = 0; column < lines[row].Length; column++)
        {
            if (Visible(trees, columns, rows, column, row))
                answer++;
        }
    }
    
    string expectedString = expected != null ? $" - Expected: {expected.Value}" : "";
    Console.WriteLine($"Answer: {answer}{expectedString}");
}

bool Visible(int[,] trees, int columns, int rows, int treeColumn, int treeRow)
{
    var treeHeight = trees[treeColumn, treeRow];
    // up
    bool visibleUp = true;
    for (int row = treeRow - 1; row >= 0; row--)
    {
        if (trees[treeColumn, row] >= treeHeight)
        {
            visibleUp = false;
            break;
        }
    }

    if (visibleUp)
        return true;
    
    // down
    bool visibleDown = true;
    for (int row = treeRow + 1; row < rows; row++)
    {
        if (trees[treeColumn, row] >= treeHeight)
        {
            visibleDown = false;
            break;
        }
    }

    if (visibleDown)
        return true;
    
    // left
    bool visibleLeft = true;
    for (int column = treeColumn - 1; column >= 0; column--)
    {
        if (trees[column, treeRow] >= treeHeight)
        {
            visibleLeft = false;
            break;
        }
    }

    if (visibleLeft)
        return true;
    
    //right
    bool visibleRight = true;
    for (int column = treeColumn + 1; column < columns; column++)
    {
        if (trees[column, treeRow] >= treeHeight)
        {
            visibleRight = false;
            break;
        }
    }

    if (visibleRight)
        return true;
    
    return false;
}

public class Tree
{
    public int Height { get; set; }
}
