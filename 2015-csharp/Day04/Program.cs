using System;
using System.Security.Cryptography;
using System.Text;

namespace Day04
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // ReSharper disable once StringLiteralTypo
            var input = "ckczppom";

            var partA = SolvePartA(input);
            Console.WriteLine($"00000 Hash is: {partA}");

            var partB = SolvePartB(input);
            Console.WriteLine($"000000 Hash is: {partB}");
        }

        public static int SolvePartA(string input)
        {
            return CalculateHash(input, "00000");
        }

        public static int SolvePartB(string input)
        {
            return CalculateHash(input, "000000");
        }

        private static int CalculateHash(string input, string prefix)
        {
            using (var hasher = MD5.Create())
            {
                for (var i = 0; i < int.MaxValue; i++)
                {
                    var data = hasher.ComputeHash(Encoding.ASCII.GetBytes(input + i));
                    var hexadecimal = BitConverter.ToString(data).Replace("-", "");
                    if (hexadecimal.StartsWith(prefix))
                        return i;
                }
            }

            throw new Exception("Exhausted all possible hashes--assume something is wrong with the code.");
        }
    }
}