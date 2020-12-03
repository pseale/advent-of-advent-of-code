module.exports.solvePartA = function (passwords) {
  let valid = 0;
  for (let i = 0; i < passwords.length; i++) {
    if (this.isPasswordValidForSledRentalPlaceDownTheRoad(passwords[i])) {
      valid++;
    }
  }
  return valid;
};

module.exports.isPasswordValidForSledRentalPlaceDownTheRoad = function (input) {
  let hits = 0;

  for (let i = 0; i < input.password.length; i++) {
    if (input.password[i] === input.policy.letter) {
      hits++;
    }
  }

  return input.policy.firstNumber <= hits && hits <= input.policy.secondNumber;
};

module.exports.parse = function (text) {
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
        firstNumber: parseInt(parts[0]),
        secondNumber: parseInt(parts[1]),
      },
      password: parts[4],
    });
  }

  return results;
};
