using Day10;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void PartA()
    {
        var input = @"[({(<(())[]>[[{[]{<()<>>
                      [(()[<>])]({[<{<<[]>>(
                      {([(<{}[<>[]}>{[]{[(<()>
                      (((({<>}<{<{<>}{[]{[]{}
                      [[<[([]))<([[{}[[()]]]
                      [{[{({}]{}}([{[{{{}}([]
                      {<[[]]>}<{[{[{[]{()[[[]
                      [<(<(<(<{}))><([]([]()
                      <{([([[(<>()){}]>(<<{{
                      <{([{{}}[<[[[<>{}]]]>[]]";

        Assert.AreEqual(6+57+1197+25137, Program.SolvePartA(input));
    }
}