using System;
using System.IO;

namespace Day25
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var partA = SolvePartA(20151125, 3029,2947);
            Console.WriteLine($"Code: {partA}");
        }

        private static long SolvePartA(long topLeftCode, int targetColumn, int targetRow)
        {
            const long multiplyBy = 252533;
            const long moduloBy = 33554393;

            Func<long, long> calculation = (c) => (c * multiplyBy) % moduloBy;

            return GetCodeFor(topLeftCode, targetColumn, targetRow, calculation);
        }

        public static long GetCodeFor(long topLeftCode, int targetColumn, int targetRow, Func<long, long> calculation)
        {
            long code = topLeftCode;
            var col = 1;
            var row = 1;
            while (true)
            {
                if (row == 1)
                {
                    // do a carriage return + line feed - back to column 1, row = current column + 1
                    row = col + 1;
                    col = 1;
                }
                else
                {
                    // move up one row, one column to the right
                    row -= 1;
                    col += 1;
                }

                code = calculation(code);

                if (col == targetColumn && row == targetRow)
                    return code;
            }
        }
    }
}