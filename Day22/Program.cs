using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public static class Program
    {
        private static readonly Spell[] Spells = new[]
        {
            new Spell("Magic Missile", 53),
            new Spell("Drain", 73),
            new Spell("Shield", 113),
            new Spell("Poison", 173),
            new Spell("Recharge", 229)
        };

        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var player = new PlayerStats(50, 500);

            var partA = SolvePartA(player, input);
            Console.WriteLine($"Least amount of mana you can spend: {partA}");
        }

        private static int SolvePartA(PlayerStats player, string input)
        {
            var boss = Parse(input);
            var spellCombinations = Combinations(player, boss);
            return spellCombinations
                .Select(spells => RunCombat(player, boss, spells))
                .Where(x => x.Victory)
                .Select(x => x.ManaCost)
                .Min();
        }

        private static List<string[]> Combinations(PlayerStats player, BossStats boss)
        {
            return new List<string[]>() { new string[0] };
        }

        private static BossStats Parse(string input)
        {
            var stats = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToDictionary(x => x.Split(":")[0], x => x.Split(" ")[^1]);

            var hitPoints = int.Parse(stats["Hit Points"]);
            var damage = int.Parse(stats["Damage"]);
            return new BossStats(hitPoints, damage);
        }
        public static CombatResult RunCombat(PlayerStats player, BossStats boss, string[] spells)
        {
            var log = new List<string>();
            var playerHitPoints = player.HitPoints;
            var playerMana = player.ManaPoints;
            var bossHitPoints = boss.HitPoints;

            var shieldActive = false;

            var manaCost = 0;
            var spellIndex = 0;

            while (true)
            {
                log.Add("-- Player turn --");
                log.Add($"Player has {playerHitPoints} hit points, 0 armor, {playerMana} mana");
                log.Add($"Boss has {bossHitPoints} hit points");
                var spell = Spells.Single(x => x.Name == spells[spellIndex]);
                switch (spell.Name)
                {
                    case "Magic Missile":
                        log.Add($"Player casts Magic Missile, dealing 4 damage.");
                        break;
                    case "Drain":
                        case "Shield":
                        case "Poison":
                        case "Recharge":
                        default: throw new Exception($"Unhandled spell: {spell.Name}");
                }


                log.Add("-- Boss turn --");
                log.Add($"Player has {playerHitPoints} hit points, 0 armor, {playerMana} mana");
                log.Add($"Boss has {bossHitPoints} hit points");
                // apply effects
                var damageFormula = shieldActive ? $"{boss.Damage} - 7 = 1" : boss.Damage.ToString();
                log.Add($"Boss attacks for {damageFormula} damage!");

                spellIndex++;
            }
            return new CombatResult(true, 0, log.ToArray());
        }
    }

    public record CombatResult(bool Victory, int ManaCost, string[] Log);
    public record PlayerStats(int HitPoints, int ManaPoints);
    public record BossStats(int HitPoints, int Damage);
    public record Spell(string Name, int ManaCost);
}