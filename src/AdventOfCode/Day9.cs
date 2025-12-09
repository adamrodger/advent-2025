using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 9
    /// </summary>
    public class Day9
    {
        public long Part1(string[] input)
        {
            Point2D[] points = input.Select(i => i.Numbers<int>()).Select(n => new Point2D(n[0], n[1])).ToArray();

            return Pairs(points).Select(pair =>
                                {
                                    long width = Math.Abs(pair.Left.X - pair.Right.X) + 1;
                                    long height = Math.Abs(pair.Left.Y - pair.Right.Y) + 1;
                                    return width * height;
                                })
                                .Max();
        }

        public long Part2(string[] input)
        {
            Point2D[] points = input.Select(i => i.Numbers<int>()).Select(n => new Point2D(n[0], n[1])).ToArray();
            Edge[] edges = CalculateEdges(points);

            return Pairs(points).Where(pair => IsInside(pair.Left, pair.Right, edges))
                                .Select(pair =>
                                {
                                    long width = Math.Abs(pair.Left.X - pair.Right.X) + 1;
                                    long height = Math.Abs(pair.Left.Y - pair.Right.Y) + 1;
                                    return width * height;
                                })
                                .Max();
        }

        private static IEnumerable<(Point2D Left, Point2D Right)> Pairs(Point2D[] points)
        {
            foreach ((int i, Point2D left) in points[..^1].Enumerate())
            {
                foreach (var right in points.Skip(i + 1))
                {
                    yield return (left, right);
                }
            }
        }

        private static Edge[] CalculateEdges(Point2D[] points)
        {
            Edge[] edges = new Edge[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                Point2D start = points[i];
                Point2D end = points[(i + 1) % points.Length];
                edges[i] = new Edge(start, end);
            }

            return edges;
        }

        private static bool IsInside(Point2D left, Point2D right, Edge[] edges)
        {
            int minX = Math.Min(left.X, right.X);
            int maxX = Math.Max(left.X, right.X);
            int minY = Math.Min(left.Y, right.Y);
            int maxY = Math.Max(left.Y, right.Y);

            // check to see if any edge intersects with the rectangle formed by the two given points
            foreach (Edge edge in edges)
            {
                if (edge.IsHorizontal)
                {
                    if (minY < edge.Start.Y && edge.Start.Y < maxY)
                    {
                        if (edge.MinX <= minX && minX < edge.MaxX || edge.MinX < maxX && maxX <= edge.MaxX)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (minX < edge.Start.X && edge.Start.X < maxX)
                    {
                        if (edge.MinY <= minY && minY < edge.MaxY || edge.MinY < maxY && maxY <= edge.MaxY)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private record Edge(Point2D Start, Point2D End, int MinX, int MaxX, int MinY, int MaxY, bool IsHorizontal)
        {
            public Edge(Point2D start, Point2D end) : this(
                start,
                end,
                Math.Min(start.X, end.X),
                Math.Max(start.X, end.X),
                Math.Min(start.Y, end.Y),
                Math.Max(start.Y, end.Y),
                start.Y == end.Y)
            {
            }
        }
    }
}
