using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public static class Program
    {
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
            var spellCombinations = GetAllCombinations(player.ManaPoints);
            var combats = spellCombinations
                .Select(spells => RunCombat(player, boss, spells))
                .ToArray();

            var victories = combats
                .Where(x => x.Victory)
                .ToArray();
            if (!victories.Any())
                throw new Exception("No victories found.");
            return victories
                .Select(x => x.ManaCost)
                .Min();
        }

        private static List<string[]> GetAllCombinations(int manaPoints)
        {
            var catalog = ISpell.GetSpellCatalog();

            var candidates = GetNextSpellCandidates(manaPoints, catalog, new List<string>());
            return Combinations(manaPoints, catalog, candidates, new List<string>()).ToList();
        }

        private static IEnumerable<string[]> Combinations(int manaPoints, SpellCatalogItem[] catalog, List<string> candidates, List<string> spellsCast)
        {
            foreach (var candidate in candidates)
            {
                var manaSpent = catalog.Single(x => x.Name == candidate).ManaCost;

                var newSpellsCast = new List<string>(spellsCast).Append(candidate).ToList();
                var manaRemaining = manaPoints - manaSpent;
                var newCandidates = GetNextSpellCandidates(manaRemaining, catalog, newSpellsCast);
                if (!newCandidates.Any())
                    yield return newSpellsCast.ToArray();

                foreach (var combination in Combinations(manaRemaining, catalog, newCandidates, newSpellsCast))
                    yield return combination;
            }
        }

        private static List<string> GetNextSpellCandidates(int manaPoints, SpellCatalogItem[] catalog, List<string> spellsCast)
        {
            var active = new List<string>();
            var currentRound = spellsCast.Count * 2;
            var rechargeRound = -1;
            for (int i = 0; i < spellsCast.Count; i++)
            {
                var round = i * 2;
                if (catalog.Single(x => x.Name == spellsCast[i]).Duration + round > currentRound)
                    active.Add(spellsCast[i]);
                if (spellsCast[i] == "Recharge")
                    rechargeRound = i * 2;

            }

            var remainingRechargeRounds =
                active.Contains("Recharge") ? 5 - (currentRound - rechargeRound) : 0;
            var manaPointsAccountingForRecharge = manaPoints + Math.Clamp(remainingRechargeRounds, 0, 2) * 101;
            var affordable = catalog.Where(x => x.ManaCost <= manaPointsAccountingForRecharge);

            var affordableAndNotActive = affordable.Where(x => !active.Contains(x.Name));
            return affordableAndNotActive
                .OrderByDescending(x => x.Name == "Recharge" ? 1 : 0) // prioritize Recharge, so I can find these logs. It's clear this should be highest priority
                .ThenByDescending(x => x.Duration)
                .Select(x => x.Name)
                .ToList();
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
            var combat = new Combat()
            {
                PlayerHitPoints = player.HitPoints,
                PlayerArmor = 0,
                PlayerManaPoints = player.ManaPoints,
                PlayerSpells = new Queue<string>(spells),

                BossHitPoints = boss.HitPoints,
                BossDamage = boss.Damage,
            };

            while (true)
            {
                var done1 = RunCombatRound(combat, true);
                if (done1) return new CombatResult(combat.PlayerHitPoints > 0, combat.ManaSpent, combat.Log.ToArray());

                var done2 = RunCombatRound(combat, false);
                if (done2) return new CombatResult(combat.PlayerHitPoints > 0, combat.ManaSpent, combat.Log.ToArray());
            }
        }

        private static bool RunCombatRound(Combat combat, bool playerRound)
        {
            var playerOrBoss = playerRound ? "Player" : "Boss";
            combat.Log.Add($"-- {playerOrBoss} turn --");
            combat.Log.Add(combat.PlayerStatusLog());
            combat.Log.Add(combat.BossStatusLog());

            foreach (var activeSpell in combat.ActiveSpells)
            {
                activeSpell.OnTick(combat);
                if (combat.BossHitPoints <= 0)
                {
                    combat.Log.Add("This kills the boss, and the player wins.");
                    return true;
                }
            }

            var wornOff = combat.ActiveSpells.Where(x => x.RoundCast + x.Duration <= combat.Round).ToArray();
            foreach (var spell in wornOff)
            {
                spell.OnWearsOff(combat);
                combat.ActiveSpells.Remove(spell);
            }

            if (playerRound)
            {
                if (!combat.PlayerSpells.Any())
                {
                    combat.Log.Add("Player is out of mana, and cannot cast a spell. Player sits idle.");
                }
                else
                {
                    var nextSpell = combat.PlayerSpells.Dequeue();
                    var spell = ISpell.Cast(nextSpell, combat.Round);
                    spell.OnCast(combat);
                    if (combat.BossHitPoints <= 0)
                    {
                        combat.Log.Add("This kills the boss, and the player wins.");
                        return true;
                    }

                    if (!spell.Immediate)
                        combat.ActiveSpells.Add(spell);
                }
            }
            else
            {
                var netDamage = combat.BossDamage - combat.PlayerArmor;
                combat.PlayerHitPoints -= netDamage;
                var damageCalculation = combat.PlayerArmor > 0
                    ? $"{combat.BossDamage} - {combat.PlayerArmor} = {netDamage}"
                    : combat.BossDamage.ToString();
                combat.Log.Add($"Boss attacks for {damageCalculation} damage!");
                if (combat.PlayerHitPoints <= 0)
                {
                    combat.Log.Add("This kills the player, and the player loses.");
                    return true;
                }
            }

            combat.Round++;
            return false;
        }
    }

    public record CombatResult(bool Victory, int ManaCost, string[] Log);
    public record PlayerStats(int HitPoints, int ManaPoints);
    public record BossStats(int HitPoints, int Damage);
}