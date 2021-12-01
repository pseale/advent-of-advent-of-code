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

            var partA = SolvePartA(input);
            Console.WriteLine($"Lowest house number: {partA}");
        }

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