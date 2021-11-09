using System.Linq;
using Day21;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void PartA()
        {
            var input = @"Hit Points: 109
                          Damage: 8
                          Armor: 2";
            // boss' stats are hardcoded
            Assert.AreEqual(0, Program.SolvePartA(input));
        }

        [Test]
        public void BattleLogShouldBeAccurate()
        {
            var input = @"Hit Points: 109
                          Damage: 8
                          Armor: 2";
            var log = Program.SimulateBattle(input);

            var expected = @"The player deals 5-2 = 3 damage; the boss goes down to 9 hit points.
                             The boss deals 7-5 = 2 damage; the player goes down to 6 hit points.
                             The player deals 5-2 = 3 damage; the boss goes down to 6 hit points.
                             The boss deals 7-5 = 2 damage; the player goes down to 4 hit points.
                             The player deals 5-2 = 3 damage; the boss goes down to 3 hit points.
                             The boss deals 7-5 = 2 damage; the player goes down to 2 hit points.
                             The player deals 5-2 = 3 damage; the boss goes down to 0 hit points.";
            var expectedLog = expected
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            CollectionAssert.AreEqual(expectedLog, log);
        }
    }
}