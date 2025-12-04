
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 4
    /// </summary>
    public class Day4
    {
        private const char Paper = '@';
        private const char Empty = '.';

        public int Part1(string[] input)
        {
            char[,] grid = input.ToGrid();
            return grid.Points().Count(p => IsAccessible(grid, p));
        }

        public int Part2(string[] input)
        {
            char[,] grid = input.ToGrid();

            int removed = 0;

            while (true)
            {
                int oldRemoved = removed;

                foreach (Point2D point in grid.Points().Where(p => IsAccessible(grid, p)))
                {
                    grid[point.Y, point.X] = Empty;
                    removed++;
                }

                if (removed == oldRemoved)
                {
                    break;
                }
            }

            return removed;
        }

        private static bool IsAccessible(char[,] grid, Point2D p)
        {
            return grid.At(p) == Paper && grid.Adjacent8(p).Count(c => c == Paper) < 4;
        }
    }
}
