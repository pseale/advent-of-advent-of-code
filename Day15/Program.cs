using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32.SafeHandles;

namespace Day15
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Highest-scoring cookie: {partA}");
        }

        // NOTE: I cheated and looked up solutions. I figured: no way people would brute force this, right? (They did.)
        public static int SolvePartA(string input)
        {
            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            // let's pretend I didn't just hardcode this, by adding some code that looks dynamic-ish.
            var ingredients = lines.Select(line => Parse(line)).ToArray();
            var sugar = ingredients.Single(x => x.Name == "Sugar");
            var sprinkles = ingredients.Single(x => x.Name == "Sprinkles");
            var candy = ingredients.Single(x => x.Name == "Candy");
            var chocolate = ingredients.Single(x => x.Name == "Chocolate");

            var scores = new List<int>();

            // NOTE: IT BOTHERS ME DEEPLY that other people's similar solutions (from
            // which I cheated 👍) ASSUME you have 4 ingredients. And ALSO handwave over
            // their off-by-one errors where they assume an optimal ingredient mixture would NEVER
            // be 100 teaspoons of a single ingredient. They're right, but not TECHNICALLY right!
            // Only REALISTICALLY.
            for (int tspSugar = 0; tspSugar <= 100; tspSugar++)
                for (int tspSprinkles = 0; tspSprinkles <= 100; tspSprinkles++)
                for (int tspCandy = 0; tspCandy <= 100; tspCandy++)
                {
                    var tspChocolate = 100 - tspCandy - tspSprinkles - tspSugar;
                    if (tspChocolate < 0 || tspChocolate > 100)
                        continue;

                    var capacity = sugar.Capacity * tspSugar
                                   + sprinkles.Capacity * tspSprinkles
                                   + candy.Capacity * tspCandy
                                   + chocolate.Capacity * tspChocolate;

                    var durability = sugar.Durability * tspSugar
                                   + sprinkles.Durability * tspSprinkles
                                   + candy.Durability * tspCandy
                                   + chocolate.Durability * tspChocolate;

                    var flavor = sugar.Flavor * tspSugar
                                   + sprinkles.Flavor * tspSprinkles
                                   + candy.Flavor * tspCandy
                                   + chocolate.Flavor * tspChocolate;

                    var texture = sugar.Texture * tspSugar
                                   + sprinkles.Texture * tspSprinkles
                                   + candy.Texture * tspCandy
                                   + chocolate.Texture * tspChocolate;
                    var score = Math.Max(0, capacity)
                                * Math.Max(0, durability)
                                * Math.Max(0, flavor)
                                * Math.Max(0, texture);

                    if (score > 0)
                        scores.Add(score);
                }

            return scores.Max();
        }

        private static Ingredient Parse(string line)
        {
            // Sugar: capacity 3, durability 0, flavor 0, texture -3, calories 2
            var name = line.Split(":")[0];

            var words = line
                .Split(":")[1]
                .Split(" ")
                .Select(x => x.Replace(",", "").Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            // capacity 3 durability 0 flavor 0 texture -3 calories 2
            return new Ingredient(name, int.Parse(words[1]), int.Parse(words[3]), int.Parse(words[5]),
                int.Parse(words[7]), int.Parse(words[9]));
        }

        record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
    }
}