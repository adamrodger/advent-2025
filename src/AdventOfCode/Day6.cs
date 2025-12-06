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
            string[] operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return operations.Select((o, i) => PerformSum(o, operands.Select(c => c[i]))).Sum();
        }

        public long Part2(string[] input)
        {
            var lines = input[..^1];
            var operands = new List<int>();
            long total = 0;

            string[] operations = input.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int sumIndex = 0;

            for (int i = 0; i < input[0].Length; i++)
            {
                ReadOnlySpan<char> verticalSlice = lines.Select(c => c[i]).Where(char.IsAsciiDigit).ToArray();

                if (verticalSlice.IsEmpty)
                {
                    // start a new sum
                    total += PerformSum(operations[sumIndex++], operands);
                    operands.Clear();
                    continue;
                }

                operands.Add(int.Parse(verticalSlice));
            }

            // finish the last sum
            total += PerformSum(operations[sumIndex], operands);

            return total;
        }

        private static long PerformSum(string operation, IEnumerable<int> operands)
            => operation == "+"
                   ? operands.Sum()
                   : operands.Aggregate(1L, (l, r) => l * r);
    }
}
