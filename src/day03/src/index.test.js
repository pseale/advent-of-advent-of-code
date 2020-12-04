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
function howManyTreesDidWeHit(forest) {
  let hits = 0;
  for (let row = 0; row < forest.length; row++) {
    if (forest[row][row * 3] === "#") {
      hits++;
    }
  }
  return hits;
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
