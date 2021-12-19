using Day12;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public void PartAShorterTestCase()
    {
        var input = @"start-A
                      start-b
                      A-c
                      A-b
                      b-d
                      A-end
                      b-end";

        Assert.AreEqual(10, Program.SolvePartA(input));
    }

    [Test]
    public void PartALargerTestCases()
    {
        var input = @"dc-end
                      HN-start
                      start-kj
                      dc-start
                      dc-HN
                      LN-dc
                      HN-end
                      kj-sa
                      kj-HN
                      kj-dc";
        Assert.AreEqual(19, Program.SolvePartA(input));

        var evenLargerInput = @"fs-end
                                he-DX
                                fs-he
                                start-DX
                                pj-DX
                                end-zg
                                zg-sl
                                zg-pj
                                pj-he
                                RW-he
                                fs-DX
                                pj-RW
                                zg-RW
                                start-pj
                                he-WI
                                zg-he
                                pj-fs
                                start-RW";
        Assert.AreEqual(226, Program.SolvePartA(evenLargerInput));
    }
}