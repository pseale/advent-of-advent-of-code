using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Difference between characters of code and characters in memory: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"Difference between encoded characters and characters of code: {partB}");
        }

        public static int SolvePartA(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            int extraChars = 0;

            foreach (var line in lines)
            {
                var chars = line.ToCharArray();
                var q = new Queue<char>(chars);

                // first character must be "
                q.Dequeue();
                extraChars++;

                while (q.Count > 0)
                {
                    char c = q.Dequeue();
                    if (c == '\\')
                    {
                        extraChars++;
                        if (q.Count == 0)
                            throw new Exception($"Invalid input: \\ at the end of a line: '{line}'");

                        var next = q.Dequeue();
                        if (next == 'x')
                        {
                            var ascii1 = q.Dequeue();
                            var ascii2 = q.Dequeue();
                            extraChars += 2;
                        }
                        else if (next == '\\' || next == '"')
                        {
                            // legal
                        }
                        else
                        {
                            throw new Exception($"invalid character following a '\\': '{next}' in line {line}");
                        }
                    }
                    else if (c == '"')
                    {
                        if (q.Count > 0)
                            throw new Exception($"Invalid input: '\"' in line: '{line}'");
                        extraChars++;
                    }
                }
            }

            return extraChars;
        }

        // extremely lazy solution -- I'm aware it is lazy and not a proper escape() because it
        // assumes legal input
        public static int SolvePartB(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            int difference = 0;

            foreach (var line in lines)
            {
                var unquoted = line.Substring(1, line.Length - 2);
                difference += 4;

                difference += Regex.Matches(unquoted, @"\\x\d\d").Count * 1;

                difference +=Regex.Matches(unquoted, @"\\[^x]").Count * 2;
            }

            return difference;
        }
    }
}