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

function immediateCombinations(adapter, adapters) {
  let combinations = 0;
  if (adapters.includes(adapter + 1)) combinations++;
  if (adapters.includes(adapter + 2)) combinations++;
  if (adapters.includes(adapter + 3)) combinations++;
  return combinations;
}

// TODO: cry, then when done, solve this problem
function solvePartB(adapters) {
  console.log("crying emoji");
  adapters.concat(0).sort((x, y) => x - y);
  const maxJoltage = adapters.sort()[adapters.length - 1];
  adapters.concat(maxJoltage + 3);

  return -111111111111;
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
    expect(result).toBe(-1); // no idea what this should be yet
  });

  test("real data", () => {
    // Arrange
    const adapters = parse(realData);

    // Act
    const result = solvePartB(adapters);

    // Assert
    expect(result).toBe(-1);
  });
});

let fs = require("fs");
let realData = fs.readFileSync("./src/input.txt", "utf8");
