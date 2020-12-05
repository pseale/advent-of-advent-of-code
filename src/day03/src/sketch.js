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

  p5.draw = () => {
    // p5.clear();
    p5.textFont("monospace");
    p5.background(0);
    p5.fill(0, 100, 0);
    p5.textSize(25);
    const forestEmojiText = inputs.forest.join("\n").replaceAll("#", "🌲");
    p5.text(forestEmojiText, 0, 0);
  };
};

module.exports.init = function init(i) {
  inputs = i;
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
  new p5module(sketch);
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
};
