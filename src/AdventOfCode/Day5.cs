using System;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 5
    /// </summary>
    public class Day5
    {
        public int Part1(string[] input)
        {
            var ranges = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                              .Select(line => line.Replace('-', ' ').Numbers<long>())
                              .Select(n => (Min: n[0], Max: n[1]))
                              .ToList();

            return input.SkipWhile(x => !string.IsNullOrEmpty(x))
                        .Skip(1)
                        .Select(line => line.Numbers<long>()[0])
                        .Count(n => ranges.Any(r => n >= r.Min && n <= r.Max));
        }

        public long Part2(string[] input)
        {
            var ranges = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                              .Select(line => line.Replace('-', ' ').Numbers<long>())
                              .Select(n => (Min: n[0], Max: n[1]))
                              .OrderBy(r => r.Min)
                              .ThenBy(r => r.Max)
                              .ToList();

            // need to merge together any overlapping ranges so that we don't double count valid values
            //
            // cases:
            // 1 2 3 4
            //       4 5 6    partially overlapping
            // 
            // 1 2 3 4
            //   2 3          entirely overlapping
            // 
            // 1 2
            //       4 5 6    not overlapping
            bool merged = true;

            while (merged)
            {
                merged = false;

                for (int i = 0; i < ranges.Count - 1; i++)
                {
                    var current = ranges[i];
                    var next = ranges[i + 1];

                    if (next.Min <= current.Max)
                    {
                        merged = true;
                        var replacement = (Math.Min(current.Min, next.Min), Math.Max(current.Max, next.Max));
                        ranges[i] = replacement;
                        ranges.RemoveAt(i + 1);
                    }
                }
            }

            return ranges.Sum(r => r.Max - r.Min + 1); // +1 because ranges are inclusive
        }
    }
}
