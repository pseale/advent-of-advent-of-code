using Day05;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day05Tests
{
    [Test]
    public void PartA()
    {
        Assert.AreEqual("1002,4,3,4,99", Csvify(Program.ExecuteIntcode("1002,4,3,4,33")));
    }

    private string Csvify(int[] memory)
    {
        return string.Join(",", memory);
    }
}