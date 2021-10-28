using System;
using System.IO;
using System.Linq;

namespace Day06
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var partA = SolvePartA(input);
            Console.WriteLine($"Lit lights: {partA}");
        }

        public static int SolvePartA(string input)
        {
            var lights = new bool[1000, 1000];

            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            foreach (var line in lines)
            {
                var words = line.Split(" ");
                if (line.StartsWith("toggle"))
                {
                    Toggle(lights, ParsePoint(words[1]), ParsePoint(words[3]));
                }
                else if (line.StartsWith("turn on"))
                {
                    TurnOn(lights, ParsePoint(words[2]), ParsePoint(words[4]));
                }
                else if (line.StartsWith("turn off"))
                {
                    TurnOff(lights, ParsePoint(words[2]), ParsePoint(words[4]));
                }
                else
                {
                    throw new Exception($"Invalid input: '{line}'");
                }
            }

            int lit = 0;
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    if (lights[x, y])
                        lit++;
                }
            }
            return lit;
        }

        private static void Toggle(bool[,] lights, Point topLeft, Point bottomRight)
        {
            for (int y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (int x = topLeft.X; x <= bottomRight.X; x++)
                {
                    lights[x, y] = !lights[x, y];
                }
            }
        }

        private static void TurnOn(bool[,] lights, Point topLeft, Point bottomRight)
        {
            for (int y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (int x = topLeft.X; x <= bottomRight.X; x++)
                {
                    lights[x, y] = true;
                }
            }
        }

        private static void TurnOff(bool[,] lights, Point topLeft, Point bottomRight)
        {
            for (int y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (int x = topLeft.X; x <= bottomRight.X; x++)
                {
                    lights[x, y] = false;
                }
            }
        }

        private static Point ParsePoint(string coordinates)
        {
            var split = coordinates.Split(",");
            return new Point(int.Parse(split[0]), int.Parse(split[1]));
        }

        record Point(int X, int Y);
    }
}