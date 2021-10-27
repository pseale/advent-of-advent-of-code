using System;
using System.IO;

namespace Day02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string inputPartA = File.ReadAllText("input-part-a.txt");
            var partA = SolvePartA(inputPartA);
        }

        public static int SolvePartA(string input)
        {
            Console.WriteLine(input);
            return 0;
        }
    }


}