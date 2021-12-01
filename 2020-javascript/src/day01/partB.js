var fs = require("fs");

function isDryRun() {
  const argsLowercase = process.argv.map((x) => x.toLowerCase());
  return argsLowercase.includes("--dry-run");
}

function parse(text) {
  return text.split("\n").map((x) => parseInt(x.trim()));
}

let filename = isDryRun() ? "./sample-data.txt" : "./input.txt";

const text = fs.readFileSync(filename, "utf8");
const input = parse(text);

for (let i = 0; i < input.length; i++) {
  for (let j = i + 1; j < input.length; j++) {
    for (let k = j + 1; k < input.length; k++) {
      if (input[i] + input[j] + input[k] === 2020) {
        console.log(
          `line numbers #${i + 1},${j + 1},${k + 1}: ${input[i]} * ${
            input[j]
          } * ${input[k]} = ${input[i] * input[j] * input[k]}`
        );
      }
    }
  }
}
