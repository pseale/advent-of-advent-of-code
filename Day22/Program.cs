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
            var spellCombinations = Combinations(player, boss);
            return spellCombinations
                .Select(spells => RunCombat(player, boss, spells))
                .Where(x => x.Victory)
                .Select(x => x.ManaCost)
                .Min();
        }

        private static List<string[]> Combinations(PlayerStats player, BossStats boss)
        {
            
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

            var wornOff = combat.ActiveSpells.Where(x => x.Round + x.Duration <= combat.Round).ToArray();
            foreach (var spell in wornOff)
            {
                spell.OnWearsOff(combat);
                combat.ActiveSpells.Remove(spell);
            }

            if (playerRound)
            {
                var nextSpell = combat.PlayerSpells.Dequeue();
                var spell = SpellHelpers.Cast(nextSpell, combat.Round);
                spell.OnCast(combat);
                if (combat.BossHitPoints <= 0)
                {
                    combat.Log.Add("This kills the boss, and the player wins.");
                    return true;
                }

                if (!spell.Immediate)
                    combat.ActiveSpells.Add(spell);
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

    public class Combat
    {
        public int Round { get; set; } = 1;
        public int ManaSpent { get; set; }

        public int PlayerHitPoints { get; set; }
        public int PlayerManaPoints { get; set; }
        public int PlayerArmor { get; set; }
        public Queue<string> PlayerSpells { get; set; } = new Queue<string>();
        public List<Spell> ActiveSpells { get; } = new List<Spell>();

        public int BossHitPoints { get; set; }
        public int BossDamage { get; set; }

        public List<string> Log { get; } = new List<string>();

        // apology: it feels icky to have some (but not most) of the logic encapsulated here in this mostly-just-a-DTO object
        public string PlayerStatusLog()
        {
            return $"- Player has {Print(PlayerHitPoints)}, {PlayerArmor} armor, {PlayerManaPoints} mana";
        }

        public string BossStatusLog()
        {
            return $"- Boss has {Print(BossHitPoints)}";
        }

        private string Print(int hitPoints)
        {
            return hitPoints > 1 ? $"{hitPoints} hit points" : $"{hitPoints} hit point";
        }
    }

    public record CombatResult(bool Victory, int ManaCost, string[] Log);
    public record PlayerStats(int HitPoints, int ManaPoints);
    public record BossStats(int HitPoints, int Damage);
    public record Spell(string Name, int ManaCost, int Round, bool Immediate, int Duration, Action<Combat> OnCast, Action<Combat> OnTick, Action<Combat> OnWearsOff);


    // apology: I wanted to see if I could solve this problem without a classic C# interface/implementation.
    // I did it, but it was pretty ugly, with a bit of duplication to boot. Class-per-spell would have been ideal.
    public static class SpellHelpers
    {
        private static readonly Action<Combat> DoNothing = _ => { };

        public static Spell Cast(string name, int round)
        {
            switch (name)
            {
                case "Magic Missile":
                    return new Spell("Magic Missile", 53, round, true, 0, CastMagicMissile, DoNothing, DoNothing);
                case "Drain":
                    return new Spell("Drain", 73, round, true, 0, CastDrain, DoNothing, DoNothing);
                case "Shield":
                    return new Spell("Shield", 113, round, false, 6, CastShield, OnShieldTick, OnShieldWearsOff);
                case "Poison":
                    return new Spell("Poison", 173, round, false, 6, CastPoison, OnPoisonTick, OnPoisonWearsOff);
                case "Recharge":
                    return new Spell("Recharge", 229, round, false, 5, CastRecharge, OnRechargeTick, OnRechargeWearsOff);
                default:
                    throw new Exception($"Invalid spell name: '{name}'");
            }
        }

        private static int GetSpellTimer(Combat combat, string spell)
        {
            var shield = combat.ActiveSpells.Single(x => x.Name == spell);
            var remainingRounds = shield.Duration - (combat.Round - shield.Round);
            return remainingRounds;
        }

        private static void CastMagicMissile(Combat combat)
        {
            combat.PlayerManaPoints -= 53;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 53;
            combat.BossHitPoints -= 4;
            combat.Log.Add("Player casts Magic Missile, dealing 4 damage.");
        }

        private static void CastDrain(Combat combat)
        {
            combat.PlayerManaPoints -= 73;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 73;
            combat.BossHitPoints -= 2;
            combat.PlayerHitPoints += 2;
            combat.Log.Add("Player casts Drain, dealing 2 damage, and healing 2 hit points.");
        }

        private static void CastShield(Combat combat)
        {
            combat.PlayerManaPoints -= 113;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 113;
            combat.PlayerArmor += 7;
            combat.Log.Add("Player casts Shield, increasing armor by 7.");
        }

        private static void OnShieldTick(Combat combat)
        {
            var remainingRounds = GetSpellTimer(combat, "Shield");
            combat.Log.Add($"Shield's timer is now {remainingRounds}.");
        }

        private static void OnShieldWearsOff(Combat combat)
        {
            combat.PlayerArmor -= 7;
            combat.Log.Add("Shield wears off, decreasing armor by 7.");
        }

        private static void CastPoison(Combat combat)
        {
            combat.PlayerManaPoints -= 173;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 173;
            combat.Log.Add("Player casts Poison.");
        }

        private static void OnPoisonTick(Combat combat)
        {
            combat.BossHitPoints -= 3;
            var remainingRounds = GetSpellTimer(combat, "Poison");
            combat.Log.Add($"Poison deals 3 damage; its timer is now {remainingRounds}.");
        }

        private static void OnPoisonWearsOff(Combat combat)
        {
            combat.Log.Add("Poison wears off.");
        }

        private static void CastRecharge(Combat combat)
        {
            combat.PlayerManaPoints -= 229;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 229;
            combat.Log.Add("Player casts Recharge.");
        }

        private static void OnRechargeTick(Combat combat)
        {
            var remainingRounds = GetSpellTimer(combat, "Recharge");
            combat.PlayerManaPoints += 101;
            combat.Log.Add($"Recharge provides 101 mana; its timer is now {remainingRounds}.");
        }

        private static void OnRechargeWearsOff(Combat combat)
        {
            combat.Log.Add("Recharge wears off.");
        }

        private static readonly string[] Spells = new string[]
        {
            "Magic Missile",
            "Drain",
            "Shield",
            "Poison",
            "Recharge"
        };
    }
}