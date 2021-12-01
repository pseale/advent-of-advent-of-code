var fs = require("fs");

function parse(text) {
  return text.split("\n").map((x) => parseInt(x.trim()));
}

const text = fs.readFileSync("./sample-data.txt", "utf8");
const input = parse(text);
// const input = [1721, 979, 366, 299, 675, 1456];

for (let i = 0; i < input.length; i++) {
  for (let j = i + 1; j < input.length; j++) {
    console.log(
      i,
      j,
      input[i],
      input[j],
      input[i] + input[j],
      input[i] * input[j]
    );
  }
}
