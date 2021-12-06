using Day02;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day02Tests
{
    [Test]
    public void PartA()
    {
        var input = @"forward 5
                      down 5
                      forward 8
                      up 3
                      down 8
                      forward 2";

        Assert.AreEqual(15 * 10, Program.SolvePartA(input));
    }

    [Test]
    public void PartB()
    {
        var input = @"forward 5
                      down 5
                      forward 8
                      up 3
                      down 8
                      forward 2";

        Assert.AreEqual(15 * 60, Program.SolvePartB(input));
    }
}