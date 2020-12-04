module.exports.parse = function (input) {
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
};

module.exports.howManyTreesDidWeHit = function (forest) {
  let hits = 0;
  for (let row = 0; row < forest.length; row++) {
    if (forest[row][row * 3] === "#") {
      hits++;
    }
  }
  return hits;
};
