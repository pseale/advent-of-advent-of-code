using System;
using System.Collections;
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
            var queue = new Queue<Combat>();
            var results = new List<CombatResult>();

            var combat = new Combat()
            {
                PlayerHitPoints = player.HitPoints,
                PlayerArmor = 0,
                PlayerManaPoints = player.ManaPoints,

                BossHitPoints = boss.HitPoints,
                BossDamage = boss.Damage,
            };
            queue.Enqueue(combat);
            while (queue.Any())
            {
                RunCombatRound(combat, queue, results);
            }

            var victories = results
                .Where(x => x.Victory)
                .ToArray();
            if (!victories.Any())
                throw new Exception("No victories found.");
            return victories
                .Select(x => x.ManaCost)
                .Min();
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

        private static void RunCombatRound(Combat combat, Queue<Combat> queue, List<CombatResult> combatResults)
        {
            var playerOrBoss = combat.IsPlayerTurn ? "Player" : "Boss";
            combat.Log.Add($"-- {playerOrBoss} turn --");
            combat.Log.Add(combat.PlayerStatusLog());
            combat.Log.Add(combat.BossStatusLog());

            foreach (var activeSpell in combat.ActiveSpells)
            {
                activeSpell.OnTick(combat);
                if (combat.BossHitPoints <= 0)
                {
                    combat.Log.Add("This kills the boss, and the player wins.");
                    combatResults.Add(new CombatResult(true, combat.ManaSpent, combat.Log.ToArray()));
                    return;
                }

                var wornOff = combat.ActiveSpells.Where(x => x.RoundCast + x.Duration <= combat.Round).ToArray();
                foreach (var spell in wornOff)
                {
                    spell.OnWearsOff(combat);
                    combat.ActiveSpells.Remove(spell);
                }

                if (combat.IsPlayerTurn)
                {
                    foreach (var spellNames in GetAvailableSpells(combat))
                    {
                        var spell = ISpell.Cast(spellNames, combat.Round);
                        combat.SpellsCast.Add(spellNames);
                        spell.OnCast(combat);
                        if (combat.BossHitPoints <= 0)
                        {
                            combat.Log.Add("This kills the boss, and the player wins.");
                            combatResults.Add(new CombatResult(true, combat.ManaSpent, combat.Log.ToArray()));
                            return;
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
                        combatResults.Add(new CombatResult(false, combat.ManaSpent, combat.Log.ToArray()));
                        return;
                    }
                }

                queue.Enqueue(NextRound(combat));
            }

        }

        private static IEnumerable<string> GetAvailableSpells(Combat combat)
        {
            var affordable = ISpell.GetSpellCatalog().Where(x => x.ManaCost <= combat.PlayerManaPoints);
            var affordableAndNotActive = affordable.Where(x => !combat.ActiveSpells.Select(x => x.Name).Contains(x.Name));
            return affordableAndNotActive
                .OrderByDescending(x => x.Name == "Recharge" ? 1 : 0) // prioritize Recharge, so I can find these logs. It's clear this should be highest priority
                .ThenByDescending(x => x.Duration)
                .Select(x => x.Name)
                .ToList();
        }

        private static Combat NextRound(Combat combat)
        {
            return new Combat()
            {
                Round = combat.Round + 1,
                Log = new List<string>(combat.Log),
                ActiveSpells = new List<ISpell>(combat.ActiveSpells),
                SpellsCast = new List<string>(combat.SpellsCast),
                BossDamage = combat.BossDamage,
                ManaSpent = combat.ManaSpent,
                PlayerArmor = combat.PlayerArmor,
                BossHitPoints = combat.BossHitPoints,
                IsPlayerTurn = !combat.IsPlayerTurn,
                PlayerHitPoints = combat.PlayerHitPoints,
                PlayerManaPoints = combat.PlayerManaPoints,
            };
        }
    }

    public record CombatResult(bool Victory, int ManaCost, string[] Log);
    public record PlayerStats(int HitPoints, int ManaPoints);
    public record BossStats(int HitPoints, int Damage);
}