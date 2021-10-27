using System;
using System.Security.Cryptography;
using System.Text;

namespace Day04
{
    public class Program
    {
        static void Main(string[] args)
        {
            // ReSharper disable once StringLiteralTypo
            string input = "ckczppom";

            int partA = SolvePartA(input);
            Console.WriteLine($"Hash is: {partA}");
        }

        public static int SolvePartA(string input)
        {
            using (var hasher = MD5.Create())
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    var data = hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(input + i));
                    var hexadecimal = BitConverter.ToString(data).Replace("-", "");
                    if (hexadecimal.StartsWith("00000"))
                        return i;
                }
            }

            throw new Exception("Exhausted all possible hashes--assume something is wrong with the code.");
        }
    }
}