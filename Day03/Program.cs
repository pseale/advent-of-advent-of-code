using System;
using System.Collections.Generic;
using System.IO;

namespace Day03
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input-part-a.txt");
            int partA = SolvePartA(input);
            Console.WriteLine($"Houses receiving at least one present: {partA}");

            int partB = SolvePartB(input);
            Console.WriteLine($"Houses receiving at least one present with Santa+Robo-Santa: {partA}");
        }

        public static int SolvePartA(string input)
        {
            var visited = new HashSet<Location>();

            int x = 0;
            int y = 0;
            visited.Add(new Location(x, y));

            var moves = input.Trim().ToCharArray();
            foreach (var move in moves)
            {
                if (move == '<') x--;
                else if (move == '>') x++;
                else if (move == 'v') y++;
                else if (move == '^') y--;
                else throw new Exception($"Invalid input: '{move}'");

                visited.Add(new Location(x, y));
            }

            return visited.Count;
        }

        public static int SolvePartB(string input)
        {
            return 0;
        }

        public record Location(int X, int Y);
    }
}