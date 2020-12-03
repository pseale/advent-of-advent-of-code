const main = require("./main");

describe("parsing", () => {
  test("1-3 a: abcde", () => {
    // Arrange + Act
    const result = main.parse("1-3 a: abcde")[0];

    // Assert
    expect(result.policy).not.toBeNull();
    expect(result.policy.letter).toBe("a");
    expect(result.policy.firstNumber).toBe(1);
    expect(result.policy.secondNumber).toBe(3);

    expect(result.password).toBe("abcde");
  });

  test("multiple lines", () => {
    const result = main.parse("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc");

    expect(result.length).toBe(3);
  });
});

describe("(Part A) Sled rental place down the road's password rules", () => {
  test("all password rules", () => {
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: a")[0]
      )
    ).toBe(true);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: aa")[0]
      )
    ).toBe(true);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: aaa")[0]
      )
    ).toBe(true);

    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("2-3 a: abcde")[0]
      )
    ).toBe(false);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: aabacade")[0]
      )
    ).toBe(false);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: bcde")[0]
      )
    ).toBe(false);
  });

  test("example given", () => {
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 a: abcde")[0]
      )
    ).toBe(true);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("1-3 b: cdefg")[0]
      )
    ).toBe(false);
    expect(
      main.isPasswordValidForSledRentalPlaceDownTheRoad(
        main.parse("2-9 c: ccccccccc")[0]
      )
    ).toBe(true);
  });
});

describe("outside-in test", () => {
  test("(Part A) sample input", () => {
    const sampleInput = "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc";
    expect(main.solvePartA(main.parse(sampleInput))).toBe(2);
  });
});
