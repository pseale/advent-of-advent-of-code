using Day03;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day03Tests
{
    [Test]
    public void PartA()
    {
        var input = @"00100
                      11110
                      10110
                      10111
                      10101
                      01111
                      00111
                      11100
                      10000
                      11001
                      00010
                      01010";

        Assert.AreEqual(22*9, Program.SolvePartA(input));
    }

    [Test]
    public void PartB()
    {
        var input = @"00100
                      11110
                      10110
                      10111
                      10101
                      01111
                      00111
                      11100
                      10000
                      11001
                      00010
                      01010";

        Assert.AreEqual(10*23, Program.SolvePartB(input));
    }
}