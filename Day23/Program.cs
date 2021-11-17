using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day23
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input, "b");
            Console.WriteLine($"Value of register b: {partA}");
        }

        public static uint SolvePartA(string input, string register)
        {
            var instructions = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            var state = new State(new Dictionary<string, uint>() { { "a", 0 }, { "b", 0 }}, 0);
            while (state.Location >= 0 && state.Location < instructions.Length)
            {
                var instruction = instructions[state.Location];
                state = Execute(instruction, state);
            }

            return state.Registers[register];
        }

        private static State Execute(string instruction, State state)
        {
            var words = instruction
                .Split(" ")
                .Select(x => x.TrimEnd(','))
                .ToArray();
            switch (words[0])
            {
                case "hlf":
                    var hlfRegisters = Copy(state.Registers);
                    hlfRegisters[words[1]] /= 2;
                    return new State(hlfRegisters, state.Location + 1);
                case "tpl":
                    var tplRegisters = Copy(state.Registers);
                    tplRegisters[words[1]] = tplRegisters[words[1]] * 3;
                    return new State(tplRegisters, state.Location + 1);
                case "inc":
                    var incRegisters = Copy(state.Registers);
                    incRegisters[words[1]] = incRegisters[words[1]] + 1;
                    return new State(incRegisters, state.Location + 1);
                case "jmp":
                        return new State(state.Registers, CalculateLocation(state.Location, words[1]));
                case "jie":
                    if (state.Registers[words[1]] % 2 == 0)
                        return state with {Location = CalculateLocation(state.Location, words[2])};
                    else
                        return state with {Location = state.Location + 1};
                case "jio":
                    if (state.Registers[words[1]] == 1)
                        return state with {Location = CalculateLocation(state.Location, words[2])};
                    else
                        return state with {Location = state.Location + 1};

                default:
                    throw new Exception($"Unknown instruction on line {state.Location}: '{words[0]}'");
            }
        }

        private static int CalculateLocation(int location, string offsetString)
        {
            var sign = offsetString[0] == '+' ? 1 : -1;
            var offset = sign * int.Parse(offsetString.Substring(1));
            var newLocation = location + offset;
            if (newLocation < 0)
                throw new Exception(
                    $"Didn't expect new location to be below zero. Old location: {location} offset: {offsetString} New location: {newLocation}");
            return newLocation;
        }

        private static Dictionary<string, uint> Copy(Dictionary<string, uint> registers)
        {
            return new Dictionary<string, uint>(registers);
        }
    }

    public record State(Dictionary<string, uint> Registers, int Location);
}