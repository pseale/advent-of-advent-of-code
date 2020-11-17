// p5.js sketch
// https://editor.p5js.org/livecoding/sketches/AFr8QFxq8
const size = 50;
const borderSize = size * 0.2;
const halfOfBorderSize = borderSize / 2.0;
const verticalPixelAdjustmentForTextBecauseP5JsIsJustALittleWonky = -3;

function setup() {
  createCanvas(800, 800);
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
      height
    }
  });
}

function drawFabricClaims(fabricClaims) {
  fill(255);
  stroke(0,255,255);
  strokeWeight(borderSize);
  for (const fabricClaim of fabricClaims) {
    rect(fabricClaim.x*size, fabricClaim.y*size, fabricClaim.width*size, fabricClaim.height*size);
  }
}

function drawOverlaps(overlaps) {
  for (const overlap of overlaps) {
    fill(255,255,0);
    stroke(0,255,255);
    strokeWeight(borderSize);
    rect(overlap.x*size, overlap.y*size, size, size);
    
    textAlign(CENTER, CENTER);
    textSize(size * .8);
    strokeWeight(0);
    fill(255,0,255);
    text('✕', overlap.x * size + halfOfBorderSize, overlap.y * size + halfOfBorderSize + verticalPixelAdjustmentForTextBecauseP5JsIsJustALittleWonky, size, size);
  }  
}

function draw() {
  background(0, 50, 50);
  drawFabricClaims(parse(`#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2`));
  // drawFabricClaims(parse("#123 @ 3,2: 5x4"));
  drawOverlaps([{x:3, y:3}, {x:3, y:4}, {x:4, y:3}, {x:4, y:4}])
  
}