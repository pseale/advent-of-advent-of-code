using Day09;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day09Tests
{
    [Test]
    public void PartA()
    {
        var input = @"2199943210
                      3987894921
                      9856789892
                      8767896789
                      9899965678";

        Assert.AreEqual(2 + 1 + 6 + 6, Program.SolvePartA(input));
    }

    [Test]
    public void PartB()
    {
        var input = @"2199943210
                      3987894921
                      9856789892
                      8767896789
                      9899965678";

        Assert.AreEqual(9 * 14 * 9, Program.SolvePartB(input));
    }
}