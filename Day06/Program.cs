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
            Console.WriteLine($"Lit lights (mistranslated): {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"Total brightness: {partB}");
        }

        public static int SolvePartA(string input)
        {
            var lights = new bool[1000, 1000];

            var lines = ParseInput(input);

            foreach (var line in lines)
            {
                var words = line.Split(" ");
                if (line.StartsWith("toggle"))
                {
                    Do(lights, ParsePoint(words[1]), ParsePoint(words[3]), isLit => !isLit);
                }
                else if (line.StartsWith("turn on"))
                {
                    Do(lights, ParsePoint(words[2]), ParsePoint(words[4]), _ => true);
                }
                else if (line.StartsWith("turn off"))
                {
                    Do(lights, ParsePoint(words[2]), ParsePoint(words[4]), _ => false);
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

        public static int SolvePartB(string input)
        {
            var lights = new int[1000, 1000];

            var lines = ParseInput(input);

            foreach (var line in lines)
            {
                var words = line.Split(" ");
                if (line.StartsWith("toggle"))
                {
                    Do(lights, ParsePoint(words[1]), ParsePoint(words[3]), current => current + 2);
                }
                else if (line.StartsWith("turn on"))
                {
                    Do(lights, ParsePoint(words[2]), ParsePoint(words[4]), current => current + 1);
                }
                else if (line.StartsWith("turn off"))
                {
                    Do(lights, ParsePoint(words[2]), ParsePoint(words[4]), current => current > 0 ? current - 1 : 0);
                }
                else
                {
                    throw new Exception($"Invalid input: '{line}'");
                }
            }

            int brightness = 0;
            for (int y = 0; y < 1000; y++)
            {
                for (int x = 0; x < 1000; x++)
                {
                    brightness += lights[x, y];
                }
            }
            return brightness;
        }

        private static string[] ParseInput(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            return lines;
        }

        // is this a little too much abstraction? OR TOO LITTLE 🤔
        private static void Do<T>(T[,] lights, Point topLeft, Point bottomRight, Func<T, T> func)
        {
            for (int y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (int x = topLeft.X; x <= bottomRight.X; x++)
                {
                    lights[x, y] = func(lights[x, y]);
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