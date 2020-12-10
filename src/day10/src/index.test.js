function parse(input) {
  return input
    .split("\n")
    .map((x) => x.trim())
    .map((x) => parseInt(x))
    .filter((x) => x);
}

function getRatedJoltagesFor(joltage) {
  return [joltage + 1, joltage + 2, joltage + 3, joltage - 3];
}

function solvePartA(adapters) {
  adapters.sort((x, y) => x - y);

  // start with 0
  let adapter = 0;

  // get all combinations of adapters that "work with" each other
  const combinationsToTry = [{ joltage: adapter, chained: [] }];

  while (true) {
    if (combinationsToTry.length === 0)
      throw "We were unable to find how to use all adapters";

    const combinationToTry = combinationsToTry.shift();
    if (combinationToTry.chained.length === adapters.length) {
      const jumpsOfThree = combinationToTry.chained.filter((x) => x.jump === 3).length;
      const jumpsOfOne = combinationToTry.chained.filter((x) => x.jump === 1).length;
      return jumpsOfOne * (jumpsOfThree + 1);
    }

    for (let joltage of getRatedJoltagesFor(combinationToTry.joltage)) {
      if (
        adapters.includes(joltage) &&
        !combinationToTry.chained.map((x) => x.maleAdapter).includes(joltage)
      ) {
        const chained = combinationToTry.chained.concat({
          maleAdapter: combinationToTry.joltage,
          femaleAdapter: joltage,
          jump: Math.abs(joltage - combinationToTry.joltage),
        });
        combinationsToTry.push({ joltage, chained });
      }
    }
  }
  // if you match every single adapter, return early (i.e. don't keep calculating once you figure it out)

  return 0;
}
describe("(Part A)", () => {
  test("sample data", () => {
    // Arrange
    const adapters = parse(`16
10
15
5
1
11
7
19
6
12
4`);

    // Act
    const result = solvePartA(adapters);

    // Assert
    expect(result).toBe(7 * 5);
  });

  test("larger sample data", () => {
    // Arrange
    const adapters = parse(`
28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3
`);

    // Act
    const result = solvePartA(adapters);

    // Assert
    expect(result).toBe(22 * 10);
  });

  test("real data", () => {
    // Arrange
    const adapters = parse(realData);

    // Act
    const result = solvePartA(adapters);

    // Assert
    expect(result).toBe(7 * 5);
  });
});

let fs = require("fs");
let realData = fs.readFileSync("./src/input.txt", "utf8");
