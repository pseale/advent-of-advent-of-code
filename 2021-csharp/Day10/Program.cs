namespace Day10;

public static class Program
{
    private static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Total syntax error score: {partA}");

        var partB = SolvePartB(input);
        Console.WriteLine($"Total autocomplete score: {partB}");
    }

    public static int SolvePartA(string input)
    {
        var scores = new List<int>();

        var queue = new Queue<char>(input.ToCharArray());
        var stack = new Stack<char>();
        while (queue.Any())
        {
            var @char = queue.Dequeue();
            if (@char is ' ' or '\n' or '\r')
            {
                stack.Clear();
                continue;
            }

            if (@char is '[' or '<' or '{' or '(')
            {
                stack.Push(@char);
            }
            else if (@char is ']' or '>' or '}' or ')')
            {
               if (stack.Any() && LegallyCloses(stack.Peek(), @char))
               {
                   stack.Pop();
               }
               else
               {
                   scores.Add(Score(@char));
                   while (queue.Any() && queue.Peek() is not '\r' or '\n') queue.Dequeue();
               }
            }
        }
        return scores.Sum();
    }

    public static long SolvePartB(string input)
    {
        var scores = new List<long>();

        var queue = new Queue<char>(input.ToCharArray());
        var stack = new Stack<char>();
        while (queue.Any())
        {
            var @char = queue.Dequeue();
            if (@char is ' ' or '\n' or '\r')
            {
                var score = EmptyTheStackAndGetAutocompleteScore(stack);
                if (score != 0) scores.Add(score);
                continue;
            }

            if (@char is '[' or '<' or '{' or '(')
            {
                stack.Push(@char);
            }
            else if (@char is ']' or '>' or '}' or ')')
            {
               if (stack.Any() && LegallyCloses(stack.Peek(), @char))
               {
                   stack.Pop();
               }
               else
               {
                   stack.Clear();
                   while (queue.Any() && queue.Peek() is not '\r' or '\n') queue.Dequeue();
               }
            }
        }

        // count final line?
        if (stack.Any())
            scores.Add(EmptyTheStackAndGetAutocompleteScore(stack));
        if (scores.Count % 2 == 0) throw new Exception($"Expected odd # of incomplete lines, got {scores.Count()}");

        scores.Sort(); // apology: no one should ever use this method
        return scores[scores.Count / 2];
    }

    private static long EmptyTheStackAndGetAutocompleteScore(Stack<char> stack)
    {
        long score = 0;

        while (stack.Any())
        {
            var @char = stack.Pop();
            var charScore = GetAutocompleteScoreForChar(@char);
            score = score * 5 + charScore;
        }

        return score;
    }

    private static int GetAutocompleteScoreForChar(char @char)
    {
        if (@char == '(') return 1;
        if (@char == '[') return 2;
        if (@char == '{') return 3;
        if (@char == '<') return 4;

        throw new Exception($"Invalid char: {@char}");
    }

    private static bool LegallyCloses(char opening, char closing)
    {
        if (opening == '(') return closing == ')';
        if (opening == '[') return closing == ']';
        if (opening == '<') return closing == '>';
        if (opening == '{') return closing == '}';

        throw new Exception($"Invalid opening character: {opening}");
    }

    private static int Score(char @char)
    {
        if (@char == ')') return 3;
        if (@char == ']') return 57;
        if (@char == '}') return 1197;
        if (@char == '>') return 25137;

        throw new Exception($"Invalid closing character: {@char}");
    }
}