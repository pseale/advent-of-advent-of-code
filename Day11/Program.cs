using System;
using System.Collections.Generic;

namespace Day11
{
    public static class Program
    {
        const int LettersInAlphabet = 26;

        static void Main(string[] args)
        {
            var input = "cqjxjnds";

            var partA = SolvePartA(input);
            Console.WriteLine($"Santa's next password should be: {partA}");

            var partB = SolvePartA(partA);
            Console.WriteLine($"After the above password expired, Santa's next password should be: {partB}");
        }

        public static string SolvePartA(string input)
        {
            long passwordAsNumber = 0;

            var stack = ConvertFromLettersToNumeric(input, ref passwordAsNumber);

            while (true)
            {
                passwordAsNumber++;
                var candidatePassword = ConvertFromNumericToLetters(passwordAsNumber, input.Length);
                if (IsValid(candidatePassword))
                    return candidatePassword;
            }
        }

        private static bool IsValid(string candidate)
        {
            if (!IncludesAStraight(candidate))
                return false;

            if (ContainsConfusingLetters(candidate))
                return false;

            if (!ContainsTwoDifferentLetterPairs(candidate))
                return false;

            return true;
        }

        private static bool IncludesAStraight(string candidate)
        {
            int thirdLetter = 2; // start at third letter
            for (int i = thirdLetter; i < candidate.Length; i++)
            {
                if (NumericValue(candidate[i]) - 1 == NumericValue(candidate[i - 1])
                    && NumericValue(candidate[i]) - 2 == NumericValue(candidate[i - 2]))
                    return true;
            }

            return false;
        }

        private static bool ContainsConfusingLetters(string candidate)
        {
            if (candidate.Contains("i"))
                return true;

            if (candidate.Contains("o"))
                return true;

            if (candidate.Contains("l"))
                return true;

            return false;
        }

        private static bool ContainsTwoDifferentLetterPairs(string candidate)
        {
            var firstPairIndex = FindPair(candidate);
            if (firstPairIndex == -1)
                return false;

            var restOfPassword = candidate.Substring(firstPairIndex + 2);
            var secondPairIndex = FindPair(restOfPassword);
            if (secondPairIndex == -1)
                return false;

            return true;
        }

        private static int FindPair(string candidate)
        {
            var secondLetter = 1;
            for (int i = secondLetter; i < candidate.Length; i++)
            {
                if (candidate[i] == candidate[i - 1])
                    return i - 1;
            }

            return -1;
        }

        private static Stack<char> ConvertFromLettersToNumeric(string input, ref long passwordAsNumber)
        {
            int powerOf = 0;
            var stack = new Stack<char>(input.ToCharArray());
            while (stack.Count > 0)
            {
                var letter = stack.Pop();
                passwordAsNumber += NumericValue(letter) * (long) Math.Pow(LettersInAlphabet, powerOf);

                powerOf++;
            }

            return stack;
        }

        private static string ConvertFromNumericToLetters(long passwordAsNumber, int digits)
        {
            var stack = new Stack<char>();

            while (passwordAsNumber > 0)
            {
                var remainder = passwordAsNumber % LettersInAlphabet;
                stack.Push(LetterValue(remainder));

                passwordAsNumber = passwordAsNumber / LettersInAlphabet;
            }

            // add leading zeroes (AKA 'a')
            // so the number 0 is properly represented by aaaaaaaaa
            while (stack.Count < digits)
                stack.Push('a');


            return new string(stack.ToArray());
        }

        private static int NumericValue(char c)
        {
            return (int) c - (int) 'a';
        }

        private static char LetterValue(long passwordAsNumber)
        {
            // don't like how this silently typecasts the 'a' to an int value. NOT ONE BIT
            return (char) ('a' + passwordAsNumber);
        }
    }
}