using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 12
    /// </summary>
    public class Day12
    {
        public int Part1(string[] input)
        {
            int[] shapeAreas = input.Chunk(5)
                                    .Take(6)
                                    .Select(chunk => chunk.Sum(line => line.Count(c => c == '#')))
                                    .ToArray();

            return input.Skip(6 * 5).Count(line =>
            {
                int[] n = line.Numbers<int>();
                int canvasArea = n[0] * n[1];
                int tileArea = n[2..].Select((x, i) => x * shapeAreas[i]).Sum();
                return canvasArea > tileArea;
            });
        }
    }
}
