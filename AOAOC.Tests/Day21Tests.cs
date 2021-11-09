using System.Linq;
using Day21;
using NUnit.Framework;

namespace AOAOC.Tests
{
    [TestFixture]
    public class Day21Tests
    {
        [Test]
        public void BattleLogShouldBeAccurate()
        {
            // Arrange
            var player = new Stats(8, 5, 5);
            var boss = new Stats(12, 7, 2);

            // Act
            var (result, log) = Program.SimulateBattle(player, boss);

            // Assert
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
            Assert.AreEqual(BattleResult.Victory, result);
        }
    }
}