using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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


            var encodedLines = lines.Select(x => Encode(x));

            return encodedLines.Sum(x => x.Length) - lines.Sum(x => x.Length);
        }

        private static char[] EncodedQuote = new[] {'\\', '\"'};
        private static char[] EncodedSlash = new[] {'\\', '\\'};

        public static string Encode(string line)
        {
            var q = new Queue<char>(line.ToCharArray());
            var encoded = new List<char>();

            // add opening quote
            char openingQuote = q.Dequeue();
            if (openingQuote != '"') throw new Exception($"Invalid input: missing opening \" in line: '{line}'");
            encoded.Add('"');
            encoded.AddRange(EncodedQuote);

            while (q.Count > 1)
            {
                char c = q.Dequeue();

                if (c == '"')
                    encoded.AddRange(EncodedQuote);
                else if (c == '\\')
                    encoded.AddRange(EncodedSlash);
                else
                    encoded.Add(c);
            }

            // add ending quote
            char closingQuote = q.Dequeue();
            if (closingQuote != '"') throw new Exception($"Invalid input: missing opening \" in line: '{line}'");
            encoded.AddRange(EncodedQuote);
            encoded.Add('"');

            return new string(encoded.ToArray());
        }
    }
}