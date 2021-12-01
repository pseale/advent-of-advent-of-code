using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = 34000000;

            Console.WriteLine("WARNING: this takes roughly a minute to complete.");
            Console.WriteLine();

            var partA = SolvePartA(input);
            Console.WriteLine($"Lowest house number: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"Lowest house number (updated rules): {partB}");
        }

        // https://codeshare.io/vwXeNy thanks!
        public static int SolvePartA(int input)
        {
            var lowerBound = 1;
            var upperBound = input;
            for (int houseNumber = lowerBound; houseNumber <= upperBound; houseNumber++)
            {
                var visitingElves = GetDivisors(houseNumber)
                    .ToArray();
                var packagesDelivered = visitingElves.Select(x => x * 10).Sum();
                if (packagesDelivered >= input)
                    return houseNumber;
            }

            throw new Exception("Couldn't find a house");
        }

        // thanks https://codeshare.io/vwXeNy !
        private static int SolvePartB(int input)
        {
            var lowerBound = 1;
            var upperBound = input;
            for (int houseNumber = lowerBound; houseNumber <= upperBound; houseNumber++)
            {
                var visitingElves = GetDivisors(houseNumber)
                    .Where(x => ElfWillVisit(x, houseNumber))
                    .ToArray();
                var packagesDelivered = visitingElves.Select(x => x * 11).Sum();
                if (packagesDelivered >= input)
                    return houseNumber;
            }

            throw new Exception("Couldn't find a house");
        }

        // apology: broke out this method because I could not hold the logic in my head otherwise
        private static bool ElfWillVisit(int giftingHouse, int recipientHouse)
        {
            // filter out gifting houses that have already visited 50 houses
            if (giftingHouse * 50 < recipientHouse)
                return false;

            return true;
        }

        // https://codereview.stackexchange.com/a/237490
        private static IEnumerable<int> GetDivisors(int n)
        {
            if (n <= 0) { yield return default; }

            int iterator = (int)Math.Sqrt(n);

            for (int i = 1; i <= iterator; i++)
            {
                if (n % i == 0)
                {
                    yield return i;

                    if (i != n / i) { yield return n / i; }
                }
            }
        }
    }
}