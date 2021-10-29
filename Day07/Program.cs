using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var partA = SolvePartA(input);
            Console.WriteLine($"Signal provided to wire a: {partA["a"]}");
        }

        public static Dictionary<string, ushort> SolvePartA(string input)
        {
            var signals = new Dictionary<string, ushort>();

            var signalGates = new List<SignalGate>();
            var gates = new List<Gate>();
            var notGates = new List<NotGate>();
            var andGates = new List<AndGate>();
            var orGates = new List<OrGate>();
            var lShiftGates = new List<LShiftGate>();
            var rShiftGates = new List<RShiftGate>();

            var lines = input
                .Split("\n")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            foreach (var line in lines)
            {
                var words = line.Split(" ");

                if (Regex.IsMatch(line, @"^\d+"))
                {
                    // 123 -> x
                    signalGates.Add(new SignalGate(words[2], ushort.Parse(words[0])));
                }
                else if (words.Length == 3)
                {
                    // lx -> a
                    gates.Add(new Gate(words[2], words[0]));
                }
                else if (words[0] == "NOT")
                {
                    // NOT e -> f
                    notGates.Add(new NotGate(words[3], words[1]));
                }
                else if (words[1] == "AND")
                {
                    // x AND y -> z
                    andGates.Add(new AndGate(words[4], words[0], words[2]));
                }
                else if (words[1] == "OR")
                {
                    // x OR y -> e
                    orGates.Add(new OrGate(words[4], words[0], words[2]));
                }
                else if (words[1] == "LSHIFT")
                {
                    // x LSHIFT 2 -> f
                    lShiftGates.Add(new LShiftGate(words[4], words[0], int.Parse(words[2])));
                }
                else if (words[1] == "RSHIFT")
                {
                    // y RSHIFT 2 -> g
                    rShiftGates.Add(new RShiftGate(words[4], words[0], int.Parse(words[2])));
                }
                else
                {
                    throw new Exception($"Invalid input: '{line}'");
                }
            }

            foreach (var gate in signalGates)
            {
                signals.Add(gate.Wire, gate.Signal);
            }

            int attempts = 0;
            while (signals.Count < lines.Length)
            {
                // unary
                var gatesToAdd = gates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.UnaryOperand));
                foreach (var gate in gatesToAdd)
                    signals[gate.Wire] = signals[gate.UnaryOperand];

                var notGatesToAdd = notGates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.UnaryOperand));
                foreach (var gate in notGatesToAdd)
                    signals[gate.Wire] = (ushort)~signals[gate.UnaryOperand];

                // single-wire
                var lShiftGatesToAdd = lShiftGates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.LeftOperand));
                foreach (var gate in lShiftGatesToAdd)
                    signals[gate.Wire] = (ushort) (signals[gate.LeftOperand] << gate.RightOperand);

                var rShiftGatesToAdd = rShiftGates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.LeftOperand));
                foreach (var gate in rShiftGatesToAdd)
                    signals[gate.Wire] = (ushort) (signals[gate.LeftOperand] >> gate.RightOperand);

                // multi-wire
                var andGatesToAdd = andGates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.LeftOperand) && signals.ContainsKey(x.RightOperand));
                foreach (var gate in andGatesToAdd)
                    signals[gate.Wire] = (ushort) (signals[gate.LeftOperand] & signals[gate.RightOperand]);

                var orGatesToAdd = orGates.Where(x => !signals.ContainsKey(x.Wire) && signals.ContainsKey(x.LeftOperand) && signals.ContainsKey(x.RightOperand));
                foreach (var gate in orGatesToAdd)
                    signals[gate.Wire] = (ushort) (signals[gate.LeftOperand] | signals[gate.RightOperand]);

                if (attempts > lines.Length)
                {
                    var unsolved = gates.Select(x => x.Wire)
                        .Concat(notGates.Select(x => x.Wire))
                        .Concat(lShiftGates.Select(x => x.Wire))
                        .Concat(rShiftGates.Select(x => x.Wire))
                        .Concat(andGates.Select(x => x.Wire))
                        .Concat(orGates.Select(x => x.Wire))
                        .Where(x => !signals.ContainsKey(x));

                    throw new Exception($"Looped too many times: we should have finished after {attempts} attempts.\nSolved wires: {string.Join(",", signals.Keys)} Unsolved wires: {string.Join(",", unsolved)}");
                }
                attempts++;
            }

            return signals;
        }

        record SignalGate(string Wire, ushort Signal);
        record Gate(string Wire, string UnaryOperand);
        record NotGate(string Wire, string UnaryOperand);
        record AndGate(string Wire, string LeftOperand, string RightOperand);
        record OrGate(string Wire, string LeftOperand, string RightOperand);
        record LShiftGate(string Wire, string LeftOperand, int RightOperand);
        record RShiftGate(string Wire, string LeftOperand, int RightOperand);
    }
}