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

  function drawGlyph(p5, glyph, row, col) {
    const cellWidth = 15;
    const cellHeight = cellWidth * 2.2;

    const margin = cellWidth;

    p5.text(glyph, margin + col * cellWidth, margin + row * cellHeight);
  }

  const grassGlyphs = [".", ".", ".", ".", ",", '"', "'", "`", ";"];
  function drawGrass(p5, row, col) {
    const color =
      p5.random() > 0.2 ? p5.color(0, 100, 0) : p5.color(200, 200, 0);
    p5.fill(color);
    drawGlyph(p5, p5.random(grassGlyphs), row, col);
  }

  let hasDrawn = false;
  p5.draw = () => {
    if (hasDrawn) return;
    hasDrawn = true;
    // p5.clear();
    p5.textFont("monospace");
    p5.textAlign(p5.CENTER, p5.CENTER);
    p5.background(0);
    p5.fill(0, 100, 0);
    p5.textSize(20);

    for (let row = 0; row < inputs.forest.length; row++) {
      for (let col = 0; col < inputs.forest[row].length; col++) {
        if (
          !inputs.trees.some((tree) => tree.row === row && tree.col === col)
        ) {
          drawGrass(p5, row, col);
        }
      }
    }

    inputs.trees.forEach((tree) => {
      drawGlyph(p5, "🌲", tree.row, tree.col);
    });
  };
};

module.exports.init = function init(i) {
  inputs = i;
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
  new p5module(sketch);
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
};
