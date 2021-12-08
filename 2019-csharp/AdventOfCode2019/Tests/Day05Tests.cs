using Day05;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day05Tests
{
    [Test]
    public void PartA()
    {
        var result = Program.ExecuteIntcode("1002,4,3,4,33", 1);

        Assert.AreEqual("1002,4,3,4,99", Csvify(result.memory));
        Assert.AreEqual(-1 , result.outputValue); // output value is never set
    }

    [Test]
    public void PartB()
    {
        var input = @"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                      1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                      999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

        Assert.AreEqual(999, Run(input, 7));
        Assert.AreEqual(1000, Run(input, 8));
        Assert.AreEqual(1001, Run(input, 9));
    }

    [Test]
    public void PartBSmallerExamples()
    {
        Assert.AreEqual(1, Run("3,9,8,9,10,9,4,9,99,-1,8", 8));
        Assert.AreEqual(0, Run("3,9,8,9,10,9,4,9,99,-1,8", 9));
        Assert.AreEqual(1, Run("3,9,7,9,10,9,4,9,99,-1,8", 7));
        Assert.AreEqual(0, Run("3,9,7,9,10,9,4,9,99,-1,8", 8));
        Assert.AreEqual(1, Run("3,3,1108,-1,8,3,4,3,99", 8));
        Assert.AreEqual(0, Run("3,3,1108,-1,8,3,4,3,99", 9));
        Assert.AreEqual(1, Run("3,3,1107,-1,8,3,4,3,99", 7));
        Assert.AreEqual(0, Run("3,3,1107,-1,8,3,4,3,99", 8));
    }

    private static int Run(string input, int inputValue)
    {
        return Program.ExecuteIntcode(input, inputValue).outputValue;
    }

    private string Csvify(int[] memory)
    {
        return string.Join(",", memory);
    }
}