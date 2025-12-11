using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 11
    /// </summary>
    public class Day11
    {
        public long Part1(string[] input)
        {
            Dictionary<string, ICollection<string>> nodes = ParseInput(input);
            var cache = new Dictionary<string, long>();

            return PathsToEnd("you");

            long PathsToEnd(string node)
            {
                if (cache.TryGetValue(node, out long value))
                {
                    return value;
                }

                if (node == "out")
                {
                    return 1;
                }

                long paths = nodes[node].Sum(PathsToEnd);
                cache[node] = paths;
                return paths;
            }
        }

        public long Part2(string[] input)
        {
            Dictionary<string, ICollection<string>> nodes = ParseInput(input);
            var cache = new Dictionary<(string, bool, bool), long>();

            return PathsToEnd("svr", false, false);

            long PathsToEnd(string node, bool visitedDac, bool visitedFft)
            {
                if (node == "dac") visitedDac = true;
                if (node == "fft") visitedFft = true;

                var key = (node, visitedDac, visitedFft);

                if (cache.TryGetValue(key, out long value))
                {
                    return value;
                }

                if (node == "out")
                {
                    return visitedDac && visitedFft ? 1 : 0;
                }

                long paths = nodes[node].Sum(n => PathsToEnd(n, visitedDac, visitedFft));
                cache[key] = paths;
                return paths;
            }
        }

        private static Dictionary<string, ICollection<string>> ParseInput(string[] input)
        {
            Dictionary<string, ICollection<string>> nodes = new() { ["out"] = [] };

            foreach (string line in input)
            {
                string[] s = line.Split(' ');
                nodes[s[0][..^1]] = s[1..].ToArray();
            }

            return nodes;
        }
    }
}
