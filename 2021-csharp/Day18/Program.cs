
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

            Console.WriteLine();
            Console.WriteLine("Additional verbs available: ");
            Console.WriteLine("print       prints an AOC tree representation. For troubleshooting.");
            Console.WriteLine("            example:  Day18.exe print '[1,2]'");
            Console.WriteLine("mermaid     prints a mermaid-js-compatible flowchart. Use with mermaid-js-cli. For troubleshooting.");
            Console.WriteLine("            example:  Day18.exe mermaid '[1,2]' > a.txt; .\\node_modules\\.bin\\mmdc.cmd -i a.txt -o a.png; ii b.png");

        }
        else if (args.Length == 2)
        {
            if (args[0].ToLowerInvariant() == "print")
                Console.WriteLine(Parse(args[1]));
            else if (args[0].ToLowerInvariant() == "mermaid")
                Console.WriteLine(PrettyPrintForMermaidJs(Parse(args[1])));

        }
    }

    // see mermaid-js docs
    private static string PrettyPrintForMermaidJs(Node node)
    {
        return "flowchart TD" + Environment.NewLine + node.PrettyPrint();
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
