function parse(input) {
  const lines = input
    .split("\n")
    .map((x) => x.trim())
    .filter((x) => x !== "");

  return lines.map((x) => {
    let line = "";
    for (let i = 0; i < 32; i++) {
      line += x;
    }

    return line;
  });
}
function howManyTreesDidWeHit() {
  return 999999;
}
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
    let forest = parse(sampleInput);

    // Act
    const result = howManyTreesDidWeHit(forest);

    // Assert
    expect(result).toBe(7);
  });

  test("parsing", () => {
    // Arrange + Act
    let forest = parse(sampleInput);
    let firstLineOfForest = forest[0];

    // Assert
    expect(forest.length).toBe(11);
    expect(forest[0]).toBe(
      "..##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##.........##......."
    );
  });
});
