using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Day22;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day22Tests
    {
        [Test]
        public void Example1BattleLogShouldBeAccurate()
        {
            // Arrange
            var player = new PlayerStats(10, 250);
            var boss = new BossStats(13, 8);
            var spells = new[] {"Poison", "Magic Missile"};

            // Act
            var result = SimulateCombat(player, boss, spells);

            // Assert
            var expected = @"-- Player turn --
                             - Player has 10 hit points, 0 armor, 250 mana
                             - Boss has 13 hit points
                             Player casts Poison.

                             -- Boss turn --
                             - Player has 10 hit points, 0 armor, 77 mana
                             - Boss has 13 hit points
                             Poison deals 3 damage; its timer is now 5.
                             Boss attacks for 8 damage!

                             -- Player turn --
                             - Player has 2 hit points, 0 armor, 77 mana
                             - Boss has 10 hit points
                             Poison deals 3 damage; its timer is now 4.
                             Player casts Magic Missile, dealing 4 damage.

                             -- Boss turn --
                             - Player has 2 hit points, 0 armor, 24 mana
                             - Boss has 3 hit points
                             Poison deals 3 damage; its timer is now 3.
                             This kills the boss, and the player wins.";
            var expectedLog = expected.Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            CollectionAssert.AreEqual(expectedLog, result.Log);
            Assert.AreEqual(true, result.Victory);
            Assert.AreEqual(173 + 53, result.ManaCost);
        }

        [Test]
        public void Example2BattleLogShouldBeAccurate()
        {
            // Arrange
            var player = new PlayerStats(10, 250);
            var boss = new BossStats(14, 8);
            var spells = new[] {"Recharge", "Shield", "Drain", "Poison", "Magic Missile"};

            // Act
            var result = SimulateCombat(player, boss, spells);

            // Assert
            var expected = @"-- Player turn --
                             - Player has 10 hit points, 0 armor, 250 mana
                             - Boss has 14 hit points
                             Player casts Recharge.
                             
                             -- Boss turn --
                             - Player has 10 hit points, 0 armor, 21 mana
                             - Boss has 14 hit points
                             Recharge provides 101 mana; its timer is now 4.
                             Boss attacks for 8 damage!
                             
                             -- Player turn --
                             - Player has 2 hit points, 0 armor, 122 mana
                             - Boss has 14 hit points
                             Recharge provides 101 mana; its timer is now 3.
                             Player casts Shield, increasing armor by 7.
                             
                             -- Boss turn --
                             - Player has 2 hit points, 7 armor, 110 mana
                             - Boss has 14 hit points
                             Recharge provides 101 mana; its timer is now 2.
                             Shield's timer is now 5.
                             Boss attacks for 8 - 7 = 1 damage!
                             
                             -- Player turn --
                             - Player has 1 hit point, 7 armor, 211 mana
                             - Boss has 14 hit points
                             Recharge provides 101 mana; its timer is now 1.
                             Shield's timer is now 4.
                             Player casts Drain, dealing 2 damage, and healing 2 hit points.
                             
                             -- Boss turn --
                             - Player has 3 hit points, 7 armor, 239 mana
                             - Boss has 12 hit points
                             Recharge provides 101 mana; its timer is now 0.
                             Shield's timer is now 3.
                             Recharge wears off.
                             Boss attacks for 8 - 7 = 1 damage!
                             
                             -- Player turn --
                             - Player has 2 hit points, 7 armor, 340 mana
                             - Boss has 12 hit points
                             Shield's timer is now 2.
                             Player casts Poison.
                             
                             -- Boss turn --
                             - Player has 2 hit points, 7 armor, 167 mana
                             - Boss has 12 hit points
                             Shield's timer is now 1.
                             Poison deals 3 damage; its timer is now 5.
                             Boss attacks for 8 - 7 = 1 damage!
                             
                             -- Player turn --
                             - Player has 1 hit point, 7 armor, 167 mana
                             - Boss has 9 hit points
                             Shield's timer is now 0.
                             Poison deals 3 damage; its timer is now 4.
                             Shield wears off, decreasing armor by 7.
                             Player casts Magic Missile, dealing 4 damage.
                             
                             -- Boss turn --
                             - Player has 1 hit point, 0 armor, 114 mana
                             - Boss has 2 hit points
                             Poison deals 3 damage; its timer is now 3.
                             This kills the boss, and the player wins.";
            var expectedLog = expected.Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            CollectionAssert.AreEqual(expectedLog, result.Log);
            Assert.AreEqual(true, result.Victory);
            Assert.AreEqual(53+73+113+173+229, result.ManaCost);
        }

        // this is all so artificial, but however ugly this is, I need a test harness. So here it is
        private CombatResult SimulateCombat(PlayerStats player, BossStats boss, string[] spells)
        {
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

            var spellQueue = new Queue<string>(spells);
            queue.Enqueue(combat);
            while (queue.Any())
            {
                combat = queue.Dequeue();
                Program.RunCombatRound(combat, queue, results, (_) => new[] {spellQueue.Dequeue()});
            }

            return results.Single();
        }
    }
}