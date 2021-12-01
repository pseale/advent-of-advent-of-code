const partA = require("./partA");

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

describe("day 03", () => {
  test("(Part A) How many trees did we hit?", () => {
    // Arrange
    let forest = partA.parse(sampleInput);

    // Act
    const result = partA.howManyTreesDidWeHit(forest);

    // Assert
    expect(result).toBe(7);
  });

  test("parsing", () => {
    // Arrange + Act
    let forest = partA.parse(sampleInput);
    let firstLineOfForest = forest[0];

    // Assert
    expect(forest.length).toBe(11);
    expect(forest[0]).toBe(
      "..##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##......."
    );
  });
});
