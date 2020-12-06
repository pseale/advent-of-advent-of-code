const p5module = require("p5");

let canvas = null;
let inputs;

let width = window.innerWidth;
let height = window.innerHeight;

let sketch = (p5) => {
  p5.setup = () => {
    p5.frameRate(60);
    canvas = document.getElementsByTagName("canvas")[0];
    p5.createCanvas(width, height);
  };

  let tick = 0;
  const people = ["🧙‍♂️", "👹", "🐸", "🎃"];
  const person = p5.random(people);
  p5.draw = () => {
    tick++;

    p5.clear();
    p5.noStroke();
    p5.noFill();
    p5.rectMode(p5.CENTER);
    p5.textSize(height * 0.6);
    p5.noStroke();
    p5.noFill();
    const rowSize = 5;
    const maxRows = Math.ceil(height / rowSize);
    p5.fill(0);
    for (let row = 0; row < maxRows; row += 5) {
      p5.rect(0, (tick % (rowSize * 5)) + row * rowSize, width * 2, rowSize * 4);
    }
    p5.textAlign(p5.CENTER, p5.CENTER);

    p5.textSize(height / 7);
    p5.fill(255);
    p5.stroke(0);
    p5.text("‍️✅a", width / 2, height * 0.9);
    p5.textSize(height / 2);
    p5.text(person, width / 2, height / 2);
  };
};

module.exports.init = function init(i) {
  inputs = i;
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
  new p5module(sketch);
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
};
