using Day02;
using NUnit.Framework;

namespace Tests;

public class Day02Tests
{
    [Test]
    public void PartASampleData()
    {
        // main example
        var input = "1,9,10,3,2,3,11,0,99,30,40,50";

        Assert.AreEqual(3500, Program.SolvePartA(input));

        // smaller samples
        Assert.AreEqual(2, Program.SolvePartA("1,0,0,0,99"));
        Assert.AreEqual(2, Program.SolvePartA("2,3,0,3,99"));
        Assert.AreEqual(2, Program.SolvePartA("2,4,4,5,99,0"));
        Assert.AreEqual(30, Program.SolvePartA("1,1,1,4,99,5,6,0,99"));
    }

    [Test]
    public void PartARealData()
    {
        // main example
        var input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,9,19,1,19,5,23,1,13,23,27,1,27,6,31,2,31,6,35,2,6,35,39,1,39,5,43,1,13,43,47,1,6,47,51,2,13,51,55,1,10,55,59,1,59,5,63,1,10,63,67,1,67,5,71,1,71,10,75,1,9,75,79,2,13,79,83,1,9,83,87,2,87,13,91,1,10,91,95,1,95,9,99,1,13,99,103,2,103,13,107,1,107,10,111,2,10,111,115,1,115,9,119,2,119,6,123,1,5,123,127,1,5,127,131,1,10,131,135,1,135,6,139,1,10,139,143,1,143,6,147,2,147,13,151,1,5,151,155,1,155,5,159,1,159,2,163,1,163,9,0,99,2,14,0,0";

        Assert.AreEqual(4690667, Program.SolvePartA(input));
    }

    [Test]
    public void IntcodeMachineWorksWithSampleData()
    {
        // main example
        var input = "1,9,10,3,2,3,11,0,99,30,40,50";

        Assert.AreEqual("3500,9,10,70,2,3,11,0,99,30,40,50", Csvify(Program.ExecuteIntcode(input)));

        // smaller samples
        Assert.AreEqual("2,0,0,0,99", Csvify(Program.ExecuteIntcode("1,0,0,0,99")));
        Assert.AreEqual("2,3,0,6,99", Csvify(Program.ExecuteIntcode("2,3,0,3,99")));
        Assert.AreEqual("2,4,4,5,99,9801", Csvify(Program.ExecuteIntcode("2,4,4,5,99,0")));
        Assert.AreEqual("30,1,1,4,2,5,6,0,99", Csvify(Program.ExecuteIntcode("1,1,1,4,99,5,6,0,99")));
    }

    private string Csvify(int[] memory)
    {
        return string.Join(",", memory);
    }
}