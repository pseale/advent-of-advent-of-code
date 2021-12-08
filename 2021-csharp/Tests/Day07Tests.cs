using Day07;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day07Tests
{
    [Test]
    public void PartA()
    {
        var input = "16,1,2,0,4,2,7,1,2,14";

        Assert.AreEqual(37, Program.Solve(input, false));
    }

    [Test]
    public void PartB()
    {
        var input = "16,1,2,0,4,2,7,1,2,14";

        Assert.AreEqual(168, Program.Solve(input, true));
    }
}