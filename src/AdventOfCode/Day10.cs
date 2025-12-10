using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;
using Microsoft.Z3;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 10
    /// </summary>
    public class Day10
    {
        public int Part1(string[] input)
        {
            int total = 0;

            foreach (string line in input)
            {
                string[] split = line.Split(' ');

                int target = 0;

                foreach ((int i, char c) in split[0][1..].Enumerate())
                {
                    if (c == '#')
                    {
                        target += (1 << i);
                    }
                }

                List<int> buttons = [];

                foreach (string section in split.Skip(1).TakeWhile(l => l.StartsWith('(')))
                {
                    int button = 0;

                    foreach (int x in section.Numbers<int>())
                    {
                        button += 1 << x;
                    }

                    buttons.Add(button);
                }

                total += FewestButtonPresses(new Machine { Target = target, Buttons = buttons });
            }

            return total;
        }

        public int Part2(string[] input)
        {
            int total = 0;

            foreach (string line in input)
            {
                string[] split = line.Split(' ');

                List<ICollection<int>> buttons = [];

                foreach (string section in split.Skip(1).TakeWhile(l => l.StartsWith('(')))
                {
                    buttons.Add(section.Numbers<int>().ToArray());
                }

                total += FewestButtonPresses2(new Machine2
                {
                    Target = split.Last().Numbers<int>().ToArray(),
                    Buttons = buttons
                });
            }

            return total;
        }

        private static int FewestButtonPresses(Machine machine)
        {
            Queue<(int State, int Presses)> queue = new();
            HashSet<int> visited = new();

            queue.Enqueue((0, 0));

            while (queue.TryDequeue(out (int State, int Presses) current))
            {
                if (current.State == machine.Target)
                {
                    return current.Presses;
                }

                if (!visited.Add(current.State))
                {
                    // been here before in same or fewer presses
                    continue;
                }

                // try every button
                foreach (int button in machine.Buttons)
                {
                    queue.Enqueue((current.State ^ button, current.Presses + 1));
                }
            }

            throw new InvalidOperationException("Well this is embarrassing... ran out of moves");
        }

        private static int FewestButtonPresses2(Machine2 machine)
        {
            var z3 = new Context();
            Optimize opt = z3.MkOptimize();

            // create all the buttons as variables and constrain them to be >= 0
            ArithExpr[] buttonVars = Enumerable.Range(0, machine.Buttons.Count)
                                               .Select(i => z3.MkIntConst($"b_{i}"))
                                               .Cast<ArithExpr>()
                                               .ToArray();

            BoolExpr pressesNotNegative = buttonVars.Select(b => z3.MkGe(b, z3.MkInt(0)))
                                                    .Aggregate((left, right) => z3.MkAnd(left, right));
            opt.Add(pressesNotNegative);

            // for each target counter, sum up all the buttons that affect it and constrain to equal the target
            foreach ((int targetIndex, int target) in machine.Target.Enumerate())
            {
                var affectingButtons = new List<ArithExpr>();

                foreach ((int i, ICollection<int> button) in machine.Buttons.Enumerate())
                {
                    if (button.Contains(targetIndex))
                    {
                        affectingButtons.Add(buttonVars[i]);
                    }
                }

                ArithExpr totalPresses = z3.MkAdd(affectingButtons);
                opt.Add(z3.MkEq(totalPresses, z3.MkInt(target)));
            }

            // minimize the total number of button presses
            opt.MkMinimize(z3.MkAdd(buttonVars));

            if (opt.Check() != Status.SATISFIABLE)
            {
                throw new InvalidOperationException("Unable to find a solution");
            }

            return buttonVars.Select(b => ((IntNum)opt.Model.Evaluate(b)).Int).Sum();
        }

        private class Machine
        {
            /// <summary>
            /// Target state to switch on the machine
            /// </summary>
            public int Target { get; init; }

            /// <summary>
            /// Buttons, which are bit masks that can be applied to the current state to create a new state
            /// </summary>
            public ICollection<int> Buttons { get; init; }
        }

        private class Machine2
        {
            /// <summary>
            /// Target state to switch on the machine
            /// </summary>
            public IList<int> Target { get; init; }

            /// <summary>
            /// Buttons, which are the joltage counters that they increase
            /// </summary>
            public ICollection<ICollection<int>> Buttons { get; init; }
        }
    }
}
