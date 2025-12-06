using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 6
    /// </summary>
    public class Day6
    {
        public long Part1(string[] input)
        {
            IList<int[]> operands = input[..^1].Select(line => line.Numbers<int>()).ToArray();
            string[] operations = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return operations.Select((o, i) =>
                              {
                                  IEnumerable<int> vertical = operands.Select(c => c[i]);

                                  return o == "+"
                                             ? vertical.Sum()
                                             : vertical.Aggregate(1L, (l, r) => l * r);
                              })
                             .Sum();
        }

        public long Part2(string[] input)
        {
            long total = 0;

            long currentSum = 0;
            char currentOperation = ' ';

            for (int i = 0; i < input[0].Length; i++)
            {
                // parse the operand from a vertical layout with optional leading or trailing whitespace
                int operand = input[..^1].Select(c => c[i])
                                         .Where(char.IsAsciiDigit)
                                         .Aggregate(0, (o, c) => o * 10 + (c - '0'));

                if (operand == 0)
                {
                    // between sums -- no operand is ever actually 0 in the input
                    total += currentSum;
                    continue;
                }

                char newOperation = input[^1][i];

                if (newOperation != ' ')
                {
                    // started new sum
                    currentOperation = newOperation;
                    currentSum = operand;
                    continue;
                }

                // continue existing sum
                currentSum = currentOperation == '+'
                                 ? currentSum + operand
                                 : currentSum * operand;
            }

            // finish the last sum
            total += currentSum;

            return total;
        }
    }
}
