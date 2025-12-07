using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 7
    /// </summary>
    public class Day7
    {
        public int Part1(string[] input)
        {
            HashSet<int> tachyons = [input[0].IndexOf('S')];
            int splits = 0;

            foreach (string line in input.Skip(1))
            {
                HashSet<int> next = [];

                foreach (int x in tachyons)
                {
                    switch (line[x])
                    {
                        case '.':
                            // carry on straight
                            next.Add(x);
                            break;
                        case '^':
                            // split
                            splits++;
                            next.Add(x - 1);
                            next.Add(x + 1);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                tachyons = next;
            }

            return splits;
        }

        public long Part2(string[] input)
        {
            int x = input[0].IndexOf('S');
            return Paths(1, x, input, new Dictionary<(int x, int y), long>());
        }

        private static long Paths(int y, int x, string[] input, IDictionary<(int x, int y), long> cache)
        {
            if (y == input.Length)
            {
                // hit the bottom
                return 1;
            }

            if (cache.TryGetValue((x, y), out long value))
            {
                return value;
            }

            long paths = input[y][x] == '^'
                             // split
                             ? Paths(y + 1, x - 1, input, cache) + Paths(y + 1, x + 1, input, cache)
                             // carry on straight
                             : Paths(y + 1, x, input, cache);

            cache[(x, y)] = paths;
            return paths;
        }
    }
}
