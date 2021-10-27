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
            Console.WriteLine($"Houses receiving at least one present with Santa+Robo-Santa: {partB}");
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
            var visited = new HashSet<Location>();

            var start = new Location(0, 0);
            var santaLocation = start;
            var roboSantaLocation = start;

            visited.Add(santaLocation);

            var moves = input.Trim().ToCharArray();
            for (int i = 0; i < moves.Length; i += 2)
            {
                santaLocation = Move(santaLocation, moves[i]);
                roboSantaLocation = Move(roboSantaLocation, moves[i+1]);

                visited.Add(santaLocation);
                visited.Add(roboSantaLocation);
            }

            return visited.Count;
        }

        private static Location Move(Location location, char move)
        {
            if (move == '<') return new Location(location.X - 1, location.Y);
            if (move == '>') return new Location(location.X + 1, location.Y);
            if (move == 'v') return new Location(location.X, location.Y + 1);
            if (move == '^') return new Location(location.X, location.Y - 1);
            else throw new Exception($"Invalid input: '{move}'");
        }

        public record Location(int X, int Y);
    }
}