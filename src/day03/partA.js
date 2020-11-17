// p5.js sketch
// https://editor.p5js.org/livecoding/sketches/AFr8QFxq8
const canvasSize = 800;
const size = canvasSize / 8;
const borderSize = size * 0.2;
const halfOfBorderSize = borderSize / 2.0;
const verticalPixelAdjustmentForTextBecauseP5JsIsJustALittleWonky = -3;

let fabricClaims;
let overlaps;
let columns;

function oneTimeSetup() {
  fabricClaims = parse(`#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2`);
  overlaps = detectOverlapsIn(fabricClaims);
  
  // just for drawing
  columns = buildColumns()
}

function buildColumns() {
  c = []
  
  const numberOfColumns = 51;
  const columnSize = canvasSize / numberOfColumns;
  for (const column of range(0, numberOfColumns + 1)) { // two columns are partially drawn
    c = c.concat(255, 0, 0);
  }
  
  return c;
}

function getSquares(x, y, width, height) {
  squares = []
  for (const i of range(x, width)) {
    for (const j of range(y, height)) {
      squares = squares.concat({x: i, y: j});
    }
  }
  return squares;
}

function parse(lines) {
  return lines.split('\n').map(line => {
    // sample line: #123 @ 3,2: 5x4
    const words = line.split(' ');
    const x = parseInt(words[2].split(/[,:]/)[0]);
    const y = parseInt(words[2].split(/[,:]/)[1]);

    const width = parseInt(words[3].split('x')[0]);
    const height = parseInt(words[3].split('x')[1]);
    
    return {
      x,
      y,
      width,
      height,
      squares: getSquares(x, y, width, height)
    }
  });
}

function drawFabricClaims(fabricClaims) {
  for (const fabricClaim of fabricClaims) {
    // draw white background for this rectangle
    // draw bold and strong border
    fill(255);
    stroke(0);
    strokeWeight(0);
    rect(fabricClaim.x*size, fabricClaim.y*size, fabricClaim.width*size, fabricClaim.height*size);
    
    // draw pips in each square
    for (const square of fabricClaim.squares) {
      writeGlyph('·', color(200, 255, 255), square);
    }
    
    // draw bold and strong border
    fill(255, 0);
    stroke(0,255,255);
    strokeWeight(borderSize);
    rect(fabricClaim.x*size, fabricClaim.y*size, fabricClaim.width*size, fabricClaim.height*size);
  }
}

// thank you StackOverflow https://stackoverflow.com/a/19506234
function range(start, count) {
  return Array.apply(0, Array(count))
    .map((element, index) => index + start);
}

function getOverlaps(a, b) {
  return a.squares.filter(aSquare => b.squares.some(bSquare => bSquare.x === aSquare.x &&  bSquare.y === aSquare.y))
}

function detectOverlapsIn(fabricClaims) {
  overlaps = []
  for (const i of range(0, fabricClaims.length)) {
    for (const j of range(i + 1, Math.max(0, fabricClaims.length - i - 1))) {
      overlaps = overlaps.concat(getOverlaps(fabricClaims[i], fabricClaims[j]));
    }
  }
  return overlaps;
}

function writeGlyph(glyph, fillColor, square) {
    textAlign(CENTER, CENTER);
    textSize(size * .8);
  
    strokeWeight(0);
    fill(fillColor);
    text(glyph, square.x * size + halfOfBorderSize, square.y * size + halfOfBorderSize + verticalPixelAdjustmentForTextBecauseP5JsIsJustALittleWonky, size, size);
}

function drawOverlaps(overlaps) {
  for (const overlap of overlaps) {
    fill(255,255,0);
    stroke(0,255,255);
    strokeWeight(borderSize);
    rect(overlap.x*size, overlap.y*size, size, size);
    
    writeGlyph('✕', color(255,0,255), overlap);
  }  
}

function columnIsHalfwayOnLeftSide(columnStart, columnSize) {
  return columnStart + columnSize > canvasSize && columnStart < canvasSize;
}

function drawBackground(offset) {
  const columnSize = canvasSize / columns.length;
    // console.log(columns)
  for (const column of range(0, columns.length)) {
    fill(columns[column]);
    const columnStart = (column - 1) * columnSize + offset;
    if (columnIsHalfwayOnLeftSide(columnStart, columnSize)) {
      rect(columnStart - canvasSize, 0, columnSize, canvasSize);
    }
    rect(columnStart % canvasSize, 0, columnSize, canvasSize);
  }
}

// p5 native functions
function setup() {
  oneTimeSetup();
  createCanvas(800, 800);
}

let tick = 0;
function draw() {
  tick++;
  drawBackground(tick);
  drawFabricClaims(fabricClaims);
  // drawFabricClaims(parse("#123 @ 3,2: 5x4"));
  drawOverlaps(overlaps)
}

