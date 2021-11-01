using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Day12
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.json");

            var partA = SolvePartA(input);
            Console.WriteLine($"Sum of all numbers: {partA}");

            var partB = SolvePartB(input);
            Console.Write("Sum, ignoring all ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("red");
            Console.ResetColor();
            Console.WriteLine($": {partB}");
        }

        public static int SolvePartA(string input)
        {
            var jToken = JToken.Parse(input);
            return CalculateSum(jToken);
        }

        private static int SolvePartB(string input)
        {
            return -1;
        }

        private static int CalculateSum(JToken jToken)
        {
            switch (jToken.Type)
            {
                case JTokenType.Array:
                case JTokenType.Object:
                case JTokenType.Property:
                    return jToken.Children().Sum(x => CalculateSum(x));
                case JTokenType.Integer:
                    return (int) jToken;
                case JTokenType.String:
                    return 0;
                default:
                    throw new NotImplementedException($"We have not implemented handling for: {jToken.Type}");
            }
        }
    }
}