const fs = require("fs");
const main = require("./main");

function isDryRun() {
  const argsLowercase = process.argv.map((x) => x.toLowerCase());
  return argsLowercase.includes("--dry-run");
}

let filename = isDryRun() ? "./sample-data.txt" : "./input.txt";

const text = fs.readFileSync(filename, "utf8");
const input = main.parse(text);

console.log(`Part A solution: ${main.solvePartA(input)}`);
