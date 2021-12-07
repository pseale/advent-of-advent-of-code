using Day06;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day06Tests
{
    [Test]
    public void PartA()
    {
        var input = "3,4,3,1,2";

        Assert.AreEqual(5934, Program.SolvePartA(input));
    }
}