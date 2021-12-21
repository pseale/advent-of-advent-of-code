
using System.Text.RegularExpressions;

namespace Day18;

public static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Magnitude of the final sum: {partA}");
        }
        else
        {
            // assume we're parsing
            Console.WriteLine(Parse(args[0]));
        }
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var trees = lines.Select(x => Parse(x))
            .ToArray();

        return -1;
    }

    private static Node Parse(string input)
    {
        var tokens = new Queue<char>(input.ToCharArray());

        var stateStack = new Stack<State>();
        State state = null;

        while (tokens.Any()) // ASSUMES valid input 👍
        {
            char c = tokens.Dequeue();
#pragma warning disable CS8602
            switch (c)
            {
                case '[':
                    if (state != null)
                        stateStack.Push(state);
                    state = new State(LeafType.Left, null, null);
                    break;
                case ']':
                    var node = new Node(state.Left, state.Right);
                    if (stateStack.Any())
                    {
                        state = stateStack.Pop();
                        if (state.WhereAreWe == LeafType.Left)
                            state = state with {Left = new Leaf(node)};
                        else
                            state = state with {Right = new Leaf(node)};
                    }
                    else // top-level -- this is the parent
                        return node;

                    break;
                case ',':
                    state = state with {WhereAreWe = LeafType.Right};
                    break;
                case '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
                    var value = int.Parse(c.ToString());
                    if (state.WhereAreWe == LeafType.Left)
                        state = state with {Left = new Leaf(value)};
                    else
                        state = state with {Right = new Leaf(value)};
                    break;
            }
#pragma warning restore CS8602
        }

        throw new Exception("Should have exited above when we got the final ]");
    }
}

public record State(LeafType WhereAreWe, Leaf? Left, Leaf? Right);

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
    public override string ToString()
    {
        if (IsNode)
            return NodeValue.ToString();

        return IntValue.ToString();
    }
}

public enum LeafType
{
    Left,
    Right
}
public class Node
{
    public Leaf Left { get; }
    public Leaf Right { get; }

    public Node(Leaf left, Leaf? right)
    {
        Left = left;
        Right = right ?? throw new Exception("Right side can't be null");
    }

    public override string ToString()
    {
        return "[" + Left + "," + Right + "]";
    }
}