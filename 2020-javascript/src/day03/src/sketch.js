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

  let cellWidth = 36;
  let cellHeight = cellWidth * 2.2;
  function drawGlyph(p5, glyph, row, col) {
    if (glyph === "💥") {
      const glyphSize = cellWidth * 2 * 2.5;
      p5.textSize(glyphSize);
      p5.text(
        glyph,
        cellWidth + col * cellWidth,
        cellHeight + row * cellHeight + glyphSize * 0.1
      );
    } else {
      p5.textSize(cellWidth * 1.5);
      p5.text(
        glyph,
        cellWidth + col * cellWidth,
        cellHeight + row * cellHeight
      );
    }
  }

  const grassGlyphs = [".", ".", ".", ".", ",", '"', "'", ";"];
  const grassColors = [
    "rgb(0,200,0)",
    "rgb(0,200,0)",
    "rgb(0,200,0)",
    "rgb(0,150,0)",
    "rgb(0,150,0)",
    "rgb(0,150,0)",
    "rgb(0,100,0)",
    "rgb(150,150,0)",
    "rgb(100,100,100)",
  ];
  function drawGrass(p5, row, col) {
    p5.fill(p5.random(grassColors));
    drawGlyph(p5, p5.random(grassGlyphs), row, col);
  }

  function drawPartBSolution(p5) {
    const megaExplosionSize = 400;
    p5.textSize(megaExplosionSize);
    p5.text("💥", width / 2, megaExplosionSize * 0.85);
    p5.fill(p5.color(255));
    p5.textSize(megaExplosionSize / 4);
    p5.stroke(p5.color(0));
    p5.strokeWeight(10);
    p5.text(inputs.partBSolution, width / 2, megaExplosionSize * 0.75);
  }

  let hasDrawn = false;
  p5.draw = () => {
    if (hasDrawn) return;
    hasDrawn = true;

    cellWidth = inputs.useRealData ? 5 : 36;
    cellHeight = cellWidth * 2.2;

    p5.textFont("monospace");
    p5.textAlign(p5.CENTER, p5.CENTER);
    p5.background(0);
    p5.fill(0, 100, 0);

    // draw enough rows to fill the screen ONLY, OR as many rows as we have
    const rowsToDraw = Math.min(
      Math.ceil(height / cellHeight),
      inputs.forest.length
    );
    // draw enough columns to fill the screen ONLY
    const columnsToDraw = Math.ceil(width / cellWidth); // inputs.forest.length;

    for (let row = 0; row < rowsToDraw; row++) {
      for (let col = 0; col < columnsToDraw; col++) {
        if (
          !inputs.trees.some((tree) => tree.row === row && tree.col === col)
        ) {
          drawGrass(p5, row, col);
        }
      }

      inputs.trees.forEach((tree) => {
        if (tree.col <= columnsToDraw && tree.row <= rowsToDraw) {
          drawGlyph(p5, "🌲", tree.row, tree.col);
        }
      });

      inputs.collisions.forEach((collision) => {
        if (collision.col <= columnsToDraw && collision.row <= rowsToDraw) {
          drawGlyph(p5, "💥", collision.row, collision.col);
          drawGlyph(p5, "🎄", collision.row, collision.col);
        }
      });

      drawPartBSolution(p5);
    }
  };
};

module.exports.init = function init(i) {
  inputs = i;
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
  new p5module(sketch);
  // ~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~=~
};
