using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    public static class Program
    {
        private static readonly Item[] ShopItems = new[]
        {
            // ReSharper disable StringLiteralTypo
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

            var partA = SolvePartA(playerHitPoints, input);
            Console.WriteLine($"Least amount of gold you can spend: {partA}");

            var partB = SolvePartB(playerHitPoints, input);
            Console.WriteLine($"Most amount of gold you can spend (and still lose): {partB}");
        }

        private static int SolvePartB(int playerHitPoints, string input)
        {
            var boss = Parse(input);
            var combinations = Combinations(playerHitPoints);
            return combinations
                .Where(x => !Victorious(x, boss))
                .Select(x => x.Cost)
                .Max();
        }

        private static int SolvePartA(int playerHitPoints, string input)
        {
            var boss = Parse(input);
            var combinations = Combinations(playerHitPoints);
            return combinations
                .Where(x => Victorious(x, boss))
                .Select(x => x.Cost)
                .Min();
        }

        private static bool Victorious(Loadout x, Stats boss)
        {
            var (result, _) = SimulateBattle(new Stats(x.HitPoints, x.Damage, x.Armor), boss);
            return result == BattleResult.Victory;
        }

        private static List<Loadout> Combinations(int playerHitPoints)
        {
            // combination rules: exactly 1 weapon
            // optional armor (maximum of 1)
            // optional rings (maximum of 2)
            var weapons = ShopItems.Where(x => x.Type == ItemType.Weapon).ToArray();
            var armors = ShopItems.Where(x => x.Type == ItemType.Armor).ToArray();
            var rings = ShopItems.Where(x => x.Type == ItemType.Ring).ToArray();

            var weaponChoices = weapons;
            var armorChoices = armors
                .Select(x => new Item[] { x })
                .Append(Array.Empty<Item>())
                .ToArray();
            var ringChoices = GetKCombs(rings, 2)
                .Concat(GetKCombs(rings, 1))
                .Append(Array.Empty<Item>())
                .ToArray();

            var loadouts = new List<Loadout>();

            foreach (var weaponChoice in weaponChoices)
            foreach (var armorChoice in armorChoices)
            foreach (var ringChoice in ringChoices)
            {
                var damage = weaponChoice.Damage
                             + armorChoice.Sum(x => x.Damage)
                             + ringChoice.Sum(x => x.Damage);

                var armor = weaponChoice.Armor
                            + armorChoice.Sum(x => x.Armor)
                            + ringChoice.Sum(x => x.Armor);

                var cost = weaponChoice.Cost
                           + armorChoice.Sum(x => x.Cost)
                           + ringChoice.Sum(x => x.Cost);

                loadouts.Add(new Loadout(playerHitPoints, damage, armor, cost));
            }

            return loadouts;
        }

        // Adapted from https://stackoverflow.com/a/10629938
        static IEnumerable<IEnumerable<Item>> GetKCombs(IEnumerable<Item> list, int length)
        {
            if (length == 1) return list.Select(t => new Item[] { t });
            return GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => String.CompareOrdinal(o.Name, t.Last()?.Name) > 0),
                    (t1, t2) => t1.Concat(new Item[] { t2 }));
        }

        private static Stats Parse(string input)
        {
            var stats = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToDictionary(x => x.Split(":")[0], x => x.Split(" ")[^1]);

            var hitPoints = int.Parse(stats["Hit Points"]);
            var damage = int.Parse(stats["Damage"]);
            var armor = int.Parse(stats["Armor"]);
            return new Stats(hitPoints, damage, armor);
        }

        public static (BattleResult, string[]) SimulateBattle(Stats player, Stats boss)
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

            var result = playerHitPoints > 0 ? BattleResult.Victory : BattleResult.Defeat;

            return (result, log.ToArray());
        }
    }

    public enum BattleResult
    {
        Victory,
        Defeat
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Ring
    }

    public record Item(ItemType Type, string Name, int Cost, int Damage, int Armor);

    public record Stats(int HitPoints, int Damage, int Armor);
    public record Loadout(int HitPoints, int Damage, int Armor, int Cost);
}