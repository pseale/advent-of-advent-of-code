namespace Day18;

public class Node
{
    private Leaf Left { get; }
    private Leaf Right { get; }

    public Node(Leaf left, Leaf right)
    {
        Left = left;
        Right = right ?? throw new Exception("Right side can't be null");

        _id = _counter.ToString();
        _counter++;
    }

    private static int _counter = 1;
    private readonly string _id;
    public string PrettyPrintId => $"id{_id}[{Left.PrettyPrintId},{Right.PrettyPrintId}]";

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