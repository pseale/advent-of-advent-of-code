function parse(input) {
  const lines = input
    .split("\n")
    .map((x) => x.trim())
    .filter((x) => x);

  const graph = {};
  lines.forEach((line) => {
    const words = line.split(" ");
    const bagName = `${words[0]} ${words[1]}`;

    if (/contain no other bags\./.test(line)) {
      if (!(bagName in graph)) {
        graph[bagName] = {
          bagName,
          children: [],
        };
      } else {
        throw `duplicate bag found: ${bagName}`;
      }
    } else if (/contain \d+/.test(line)) {
      const children = line
        .split("contain ")[1]
        .replace(".", "")
        .split(",")
        .map((x) => x.trim())
        .split(" ")
        .map((x) => {
          return { quantity: parseInt(x[0]), bagName: `${x[1]} ${x[2]}` };
        });
      graph[bagName] = {
        bagName,
        children,
      };
    } else {
      throw `Invalid input: ${line}`;
    }
  });
  return recordParentsFor(graph);
}

function recordParentsFor(graph) {
  for (const bagName in graph) {
    graph[bagName].parents = [];
  }
  return graph;
}

function solvePartA(graph) {
  return -1;
}

describe("(Part A)", () => {
  test("sample data", () => {
    // Arrange
    const sampleData = `light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.`;
    let graph = parse(sampleData);

    // Act
    const result = solvePartA(graph);

    // Assert
    expect(result).toBe(4);
  });
});

describe("parse", () => {
  test("parsing node with no children", () => {
    // Arrange
    const input = `faded blue bags contain no other bags.
      dotted black bags contain no other bags.`;

    // Act
    const graph = parse(input);

    // Assert
    expect(Object.keys(graph)).toStrictEqual(["faded blue", "dotted black"]);

    const fadedBlue = graph["faded blue"];
    expect(fadedBlue.children.length).toBe(0);
    expect(fadedBlue.parents.length).toBe(0);

    const dottedBlack = graph["dotted black"];
    expect(dottedBlack.children.length).toBe(0);
    expect(dottedBlack.parents.length).toBe(0);
  });
});
