using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

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

                List<int> buttons = [];

                foreach (string section in split.Skip(1).TakeWhile(l => l.StartsWith('(')))
                {
                    buttons.Add(section.Numbers<int>().Select(n => (int)Math.Pow(10, n)).Sum());
                }

                int target = 0;

                foreach ((int i, int x) in split.Last().Numbers<int>().Enumerate())
                {
                    target += x * (int)Math.Pow(10, i);
                }

                total += FewestButtonPresses2(new Machine2 { Target = target, Buttons = buttons });
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

                // check if any counter has exceeded target
                /*if (AnyDigitExceeds(current.State, machine.Target))
                {
                    // no point carrying on down this path
                    continue;
                }*/

                // try every button
                foreach (int button in machine.Buttons)
                {
                    queue.Enqueue((current.State + button, current.Presses + 1));
                }
            }

            throw new InvalidOperationException("Well this is embarrassing... ran out of moves");
        }

        private static bool AnyDigitExceeds(int state, int target)
        {
            while (state > 0 || target > 0)
            {
                int currentDigit = state % 10;
                int targetDigit = target % 10;

                if (currentDigit > targetDigit)
                {
                    return true;
                }

                state /= 10;
                target /= 10;
            }

            return false;
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
            public int Target { get; init; }

            /// <summary>
            /// Buttons, which are the number to add to the current state to create a new state
            /// </summary>
            public ICollection<int> Buttons { get; init; }
        }
    }
}
