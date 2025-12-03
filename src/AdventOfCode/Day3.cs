using System;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 3
    /// </summary>
    public class Day3
    {
        public int Part1(string[] input)
        {
            int total = 0;

            foreach (string line in input)
            {
                (int first, int firstIndex) = line[..^1].Select((c, i) => (digit: c - '0', index: i)).MaxBy(pair => pair.digit);
                int last = line[(firstIndex + 1)..].Select(c => c - '0').Max();

                int score = first * 10 + last;
                total += score;
            }

            return total;
        }

        public long Part2(string[] input)
        {
            long total = 0;

            foreach (string line in input)
            {
                long score = 0;
                int start = -1;

                for (int i = 11; i >= 0; i--)
                {
                    (int digit, start) = line[(start + 1)..^i].Select((c, j) => (digit: c - '0', index: start + j + 1)).MaxBy(pair => pair.digit);
                    score += digit * (long)Math.Pow(10, i);
                }

                total += score;
            }

            return total;
        }
    }
}
