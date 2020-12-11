function parse(input) {
  return input
    .split("\n")
    .map((x) => x.trim())
    .map((x) => parseInt(x))
    .filter((x) => x);
}

// why did they do joltage - 3, it makes no sense
function getRatedJoltagesFor(joltage) {
  return [joltage + 1, joltage + 2, joltage + 3];
}

function solvePartA(adapters) {
  adapters.sort((x, y) => x - y);

  let jumpsOfOne = 0;
  let jumpsOfThree = 0;

  let adapter = 0;
  // start with 0
  while (adapter < adapters[adapters.length - 1]) {
    if (adapters.includes(adapter + 1)) {
      jumpsOfOne++;
      adapter += 1;
    } else if (adapters.includes(adapter + 3)) {
      jumpsOfThree++;
      adapter += 3;
    } else {
      throw "we should never be here in Part A";
    }
  }

  jumpsOfThree += 1; // count the final jump of 3
  return jumpsOfOne * jumpsOfThree;
}

// See, I knew this problem wasn't about programming, but math :/
// https://brilliant.org/wiki/tribonacci-sequence/
const tribonacciSequence = [1, 1, 2, 4, 7, 13, 24, 44, 81, 149];
function getTribonacci(num) {
  if (num > tribonacciSequence.length)
    throw `Can't calculate tribonacci number for ${num}`;

  return tribonacciSequence[num - 1];
}

function solvePartB(adapters) {
  const maxJoltage = adapters.sort((x, y) => x - y)[adapters.length - 1];
  const a = adapters.concat([0, maxJoltage + 3]).sort((x, y) => x - y);

  let multiplier = 1;
  let currentRun = 1;
  for (let joltage of a) {
    if (adapters.includes(joltage + 1)) {
      currentRun++;
    } else {
      multiplier *= getTribonacci(currentRun);
      currentRun = 1;
    }
  }
  return multiplier;
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
    expect(result).toBe(1690);
  });
});

describe("(Part B)", () => {
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
    const result = solvePartB(adapters);

    // Assert
    expect(result).toBe(8);
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
    const result = solvePartB(adapters);

    // Assert
    expect(result).toBe(19208);
  });

  test("real data", () => {
    // Arrange
    const adapters = parse(realData);

    // Act
    const result = solvePartB(adapters);

    // Assert
    expect(result).toBe(5289227976704);
  });
});

let fs = require("fs");
let realData = fs.readFileSync("./src/input.txt", "utf8");
