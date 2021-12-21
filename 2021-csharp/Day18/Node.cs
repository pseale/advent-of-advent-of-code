namespace Day18;

public class Node
{
    public Leaf Left { get; }
    public Leaf Right { get; }

    public string PrettyPrintId => $"{Left.PrettyPrintId},{Right.PrettyPrintId}";

    public Node(Leaf left, Leaf? right)
    {
        Left = left;
        Right = right ?? throw new Exception("Right side can't be null");
    }

    public override string ToString()
    {
        return "[" + Left + "," + Right + "]";
    }

    public string PrettyPrint()
    {
        var lines = new List<string>();
        if (Left.IsNode)
            lines.Add(PrettyPrintId + " --> " + Left.PrettyPrintId);
        if (Right.IsNode)
            lines.Add(PrettyPrintId + " --> " + Right.PrettyPrintId);
        lines.Add(Left.PrettyPrint());
        lines.Add(Right.PrettyPrint());
        return string.Join(Environment.NewLine, lines);
    }
}