const inputs = require("./inputs");

describe("(Part B)", () => {
  test("collisions", () => {
    // Arrange
    const forest = inputs.parse(sampleInput);

    // Act
    const collisions = inputs.findCollisions(forest);

    // Assert
    expect(collisions.length).toBe(2 + 7 + 3 + 4 + 2);
    debugger;
    const right3down1Collisions = collisions.filter(
      (x) => x.slope === "right-3-down-1"
    );
    expect(right3down1Collisions.length).toBe(7);
  });

  test("outside-in acceptance test", () => {
    // Arrange
    const forest = inputs.parse(sampleInput);
    const collisions = inputs.findCollisions(forest);

    // Act
    const result = inputs.solvePartB(collisions);

    // Assert
    expect(result).toBe(336);
  });
});

const sampleInput = `..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#`;
