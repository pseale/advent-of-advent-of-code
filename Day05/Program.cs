using System;
using System.IO;
using System.Linq;

namespace Day05
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            int partA = SolvePartA(input);
            Console.WriteLine($"Nice strings (legacy model): {partA}");

            int partB = SolvePartB(input);
            Console.WriteLine($"Nice strings (better model): {partB}");

        }

        public static int SolvePartB(string input)
        {
            return 0;
        }

        public static int SolvePartA(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            // ReSharper disable once ReplaceWithSingleCallToCount
            return lines.Where(x => HasThreeVowels(x))
                .Where(x => LetterAppearsTwiceInARow(x))
                .Where(x => !HasForbiddenStrings(x))
                .Count();
        }

        private static readonly char[] Vowels = new[] {'a', 'e', 'i', 'o', 'u'};
        private static bool HasThreeVowels(string @string)
        {
            // ReSharper disable once ReplaceWithSingleCallToCount
            return @string.ToCharArray().Where(c => Vowels.Contains(c)).Count() >= 3;
        }

        private static bool LetterAppearsTwiceInARow(string @string)
        {
            for (int i = 0; i < @string.Length - 1; i++)
            {
                if (@string[i] == @string[i + 1])
                    return true;
            }

            return false;
        }

        private static bool HasForbiddenStrings(string @string)
        {
            if (@string.Contains("ab")) return true;
            if (@string.Contains("cd")) return true;
            if (@string.Contains("pq")) return true;
            if (@string.Contains("xy")) return true;

            return false;
        }
    }
}