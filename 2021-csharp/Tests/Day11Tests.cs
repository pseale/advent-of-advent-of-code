using Day11;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day11Tests
{
    [Test]
    public void TestHarnessForBothPartAAndPartB()
    {
        var input = @"5483143223
                      2745854711
                      5264556173
                      6141336146
                      6357385478
                      4167524645
                      2176841721
                      6882881134
                      4846848554
                      5283751526";

        Assert.AreEqual((1656, 195), Program.Solve(input));
    }
}