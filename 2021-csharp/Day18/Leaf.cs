namespace Day18;

// apology: I wish I had rust sum types. enum { int or Node }
public class Leaf
{
    public Leaf(Node node)
    {
        IsNode = true;
        NodeValue = node;
    }

    public Leaf(int value)
    {
        IsNode = false;
        IntValue = value;
        NodeValue = null;
    }

    public bool IsNode { get; set; }
    public int IntValue { get; set; }
    public Node? NodeValue { get; set; }

    public string PrettyPrintId => IsNode ? NodeValue.PrettyPrintId : IntValue.ToString();

    public override string ToString()
    {
        if (IsNode)
            return NodeValue.ToString();

        return IntValue.ToString();
    }

    public string PrettyPrint()
    {
        if (IsNode)
            return NodeValue.PrettyPrint();

        return "";
    }
}

public enum LeafType
{
    Left,
    Right
}