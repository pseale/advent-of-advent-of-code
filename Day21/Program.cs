using System;
using System.Collections.Generic;
using System.IO;

namespace Day21
{
    public static class Program
    {
        private static readonly Item[] ShopItems = new[]
        {
            new Item(ItemType.Weapon, "Dagger", 8, 4, 0),
            new Item(ItemType.Weapon, "Shortsword", 10, 5, 0),
            new Item(ItemType.Weapon, "Warhammer", 25, 6, 0),
            new Item(ItemType.Weapon, "Longsword", 40, 7, 0),
            new Item(ItemType.Weapon, "Greataxe", 74, 8, 0),

            new Item(ItemType.Armor, "Leather", 13, 0, 1),
            new Item(ItemType.Armor, "Chainmail", 31, 0, 2),
            new Item(ItemType.Armor, "Splitmail", 53, 0, 3),
            new Item(ItemType.Armor, "Bandedmail", 75, 0, 4),
            new Item(ItemType.Armor, "Platemail", 102, 0, 5),

            new Item(ItemType.Ring, "Damage +1", 25, 1, 0),
            new Item(ItemType.Ring, "Damage +2", 50, 2, 0),
            new Item(ItemType.Ring, "Damage +3", 100, 3, 0),
            new Item(ItemType.Ring, "Defense +1", 20, 0, 1),
            new Item(ItemType.Ring, "Defense +2", 40, 0, 2),
            new Item(ItemType.Ring, "Defense +3", 80, 0, 3)
        };
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var playerHitPoints = 100;

            var partA = SolvePartA(100, input);
            Console.WriteLine($"Least amount of gold you can spend: {partA}");
        }

        public static int SolvePartA(int playerHitPoints, string input)
        {
            return -1;
        }

        public static string[] SimulateBattle(Stats player, Stats boss)
        {
            var log = new List<string>();
            int playerHitPoints = player.HitPoints;
            int bossHitPoints = boss.HitPoints;
            bool playerTurn = true;
            while (playerHitPoints > 0 && bossHitPoints > 0)
            {
                var weaponDamage = playerTurn ? player.Damage : boss.Damage;
                var targetArmor = playerTurn ? boss.Armor : player.Armor;
                var damage = weaponDamage - targetArmor;

                // simulate the round
                if (playerTurn)
                    bossHitPoints -= damage;
                else
                {
                    playerHitPoints -= damage;
                }
                var targetHitPoints = playerTurn ? bossHitPoints : playerHitPoints;

                // log the results
                var attacker = playerTurn ? "player" : "boss";
                var target = playerTurn ? "boss" : "player";
                log.Add($"The {attacker} deals {weaponDamage}-{targetArmor} = {damage} damage; the {target} goes down to {targetHitPoints} hit points.");

                playerTurn = !playerTurn;
            }

            return log.ToArray();
        }


    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Ring
    }

    public record Item(ItemType Type, string Name, int Cost, int Damage, int Armor);

    public record Stats(int HitPoints, int Damage, int Armor);
}