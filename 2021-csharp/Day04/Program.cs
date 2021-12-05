using System.Collections;

namespace Day04;

public static class Program
{
    private const int Size = 5;

    static void Main()
    {
        var input = File.ReadAllText("input.txt");

        var partA = SolvePartA(input);
        Console.WriteLine($"Final score: {partA}");
    }

    public static int SolvePartA(string input)
    {
        var lines = input
            .Split("\n")
            .Select(x => x.Trim())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        var numbers = lines[0]
            .Split(",")
            .Select(x => int.Parse(x));


        var bingoBoards = ParseBingoBoards(lines.Skip(1).ToArray());

        var queue = new Queue<int>(numbers);
        while (true)
        {
            var drawn = queue.Dequeue();
            foreach (var board in bingoBoards)
            {
                for (int col = 0; col < Size; col++)
                for (int row = 0; row < Size; row++)
                    if (board[col, row].Number == drawn)
                        board[col, row].Drawn = true;
            }

            var winner = GetWinner(bingoBoards);
            if (winner != null)
            {
                var unmarkedNumbers = CountUnmarked(winner);
                return unmarkedNumbers * drawn;
            }
        }
    }

    private static int CountUnmarked(BingoSquare[,] board)
    {
        var sum = 0;
        for (int col = 0; col < Size; col++)
            for (int row = 0; row < Size; row++)
                if (!board[col, row].Drawn)
                    sum += board[col, row].Number;
        return sum;
    }

    // apology: I hate the 'return null' pattern in general
    private static BingoSquare[,] GetWinner(List<BingoSquare[,]> bingoBoards)
    {
        foreach (var board in bingoBoards)
        {
            // vertical winner
            for (int col = 0; col < Size; col++)
            {
                if (board[0, col].Drawn && board[1, col].Drawn && board[2, col].Drawn && board[3, col].Drawn &&
                    board[4, col].Drawn)
                    return board;

            }
            // horizontal winner
            for (int row = 0; row < Size; row++)
            {
                if (board[row, 0].Drawn && board[row, 1].Drawn && board[row, 2].Drawn && board[row, 3].Drawn &&
                    board[row, 4].Drawn)
                    return board;

            }
        }

        return null;
    }

    private static List<BingoSquare[,]> ParseBingoBoards(string[] lines)
    {
        if (lines.Length % Size != 0) throw new Exception($"Expected lines to be a multiple of 5, but was {lines.Length}");

        var bingoBoards = new List<BingoSquare[,]>();

        var chunks = lines.Chunk(Size);

        // one chunk is:
        //
        // 22 13 17 11  0
        // 8  2 23  4 24
        // 21  9 14 16  7
        // 6 10  3 18  5
        // 1 12 20 15 19
        foreach (var chunk in chunks)
        {
            var board = new BingoSquare[Size, Size];
            for (var row = 0; row < Size; row++)
            {
                var numbers = chunk[row]
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x))
                    .ToArray();

                for (var col = 0; col < Size; col++)
                {
                    board[col, row] = new BingoSquare() {Number = numbers[col]};
                }
            }

            bingoBoards.Add(board);
        }

        return bingoBoards;
    }

    public class BingoSquare
    {
        public int Number { get; set; }
        public bool Drawn { get; set; }
    }
}


