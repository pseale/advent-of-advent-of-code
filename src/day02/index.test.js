function isPasswordValid(input) {
  let hits = 0;

  for (let i = 0; i < input.password.length; i++) {
    if (input.password[i] === input.policy.letter) {
      hits++;
    }
  }

  return input.policy.atLeast <= hits && hits <= input.policy.atMost;
}

function parse(text) {
  const lines = text.split("\n").map((x) => x.trim());

  const results = [];
  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // I don't apologize for the regex. It felt great
    // 1-3 a: abcde      splits into [ '1', '3', 'a', '', 'abcde' ]
    const parts = line.split(/[\s:-]/);
    results.push({
      policy: {
        letter: parts[2],
        atLeast: parseInt(parts[0]),
        atMost: parseInt(parts[1]),
      },
      password: parts[4],
    });
  }

  return results;
}

describe("parsing", () => {
  test("1-3 a: abcde", () => {
    // Arrange + Act
    const result = parse("1-3 a: abcde")[0];

    // Assert
    expect(result.policy).not.toBeNull();
    expect(result.policy.letter).toBe("a");
    expect(result.policy.atLeast).toBe(1);
    expect(result.policy.atMost).toBe(3);

    expect(result.password).toBe("abcde");
  });

  test("multiple lines", () => {
    const result = parse("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc");

    expect(result.length).toBe(3);
  });
});

describe("password rules", () => {
  test("all password rules", () => {
    debugger;
    expect(isPasswordValid(parse("1-3 a: a")[0])).toBe(true);
    expect(isPasswordValid(parse("1-3 a: aa")[0])).toBe(true);
    expect(isPasswordValid(parse("1-3 a: aaa")[0])).toBe(true);

    expect(isPasswordValid(parse("2-3 a: abcde")[0])).toBe(false);
    expect(isPasswordValid(parse("1-3 a: aabacade")[0])).toBe(false);
    expect(isPasswordValid(parse("1-3 a: bcde")[0])).toBe(false);
  });

  test("example given", () => {
    expect(isPasswordValid(parse("1-3 a: abcde")[0])).toBe(true);
    expect(isPasswordValid(parse("1-3 b: cdefg")[0])).toBe(false);
    expect(isPasswordValid(parse("2-9 c: ccccccccc")[0])).toBe(true);
  });
});
