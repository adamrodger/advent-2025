using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 8
    /// </summary>
    public class Day8
    {
        public long Part1(string[] input, int connections = 1000)
        {
            (JunctionBox[] junctions, List<JunctionPair> distances) = ParseJunctionBoxes(input);

            // every junction box starts in its own circuit
            List<HashSet<JunctionBox>> circuits = junctions.Select(j => new HashSet<JunctionBox> { j }).ToList();
            Dictionary<int, HashSet<JunctionBox>> index = circuits.ToDictionary(c => c.First().Id);

            // make the configured number of connections
            foreach ((_, JunctionBox left, JunctionBox right)  in distances.Take(connections))
            {
                HashSet<JunctionBox> dest = index[left.Id];
                HashSet<JunctionBox> src = index[right.Id];

                if (dest == src)
                {
                    // already connected
                    continue;
                }

                circuits.Remove(src);

                // not already connected, merge the two circuits
                foreach (JunctionBox jb in src)
                {
                    dest.Add(jb);
                    index[jb.Id] = dest;
                }
            }

            // the product of the top 3 longest circuits
            return circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1L, (l, set) => l * set.Count);
        }

        public long Part2(string[] input)
        {
            (JunctionBox[] junctions, List<JunctionPair> distances) = ParseJunctionBoxes(input);

            var queue = new Queue<JunctionPair>(distances);

            // every junction box starts in its own circuit
            List<HashSet<JunctionBox>> circuits = junctions.Select(j => new HashSet<JunctionBox> { j }).ToList();
            Dictionary<int, HashSet<JunctionBox>> index = circuits.ToDictionary(c => c.First().Id);

            while (queue.TryDequeue(out JunctionPair current))
            {
                HashSet<JunctionBox> dest = index[current.Left.Id];
                HashSet<JunctionBox> src = index[current.Right.Id];

                if (dest == src)
                {
                    // already connected
                    continue;
                }

                circuits.Remove(src);

                // not already connected, merge the two circuits
                foreach (JunctionBox jb in src)
                {
                    dest.Add(jb);
                    index[jb.Id] = dest;
                }

                if (circuits.Count == 1)
                {
                    // joined everything together into one big circuit
                    return current.Left.Location.X * current.Right.Location.X;
                }
            }

            throw new InvalidOperationException("Ran out of connections to make before joining everything");
        }

        /// <summary>
        /// Parse all junction boxes from the input and calculate pairwise distances, sorted by descending distance
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns>Parsed junction boxes and pairwise distances</returns>
        private static (JunctionBox[], List<JunctionPair>) ParseJunctionBoxes(string[] input)
        {
            JunctionBox[] junctions = input.Select(i => i.Numbers<int>())
                                           .Select((n, i) => new JunctionBox(i, new Point3D(n[0], n[1], n[2])))
                                           .ToArray();

            // calculate the pairwise distance between all junctions
            var distances = new List<JunctionPair>();

            foreach ((int i, JunctionBox current) in junctions[..^1].Enumerate())
            {
                foreach (JunctionBox other in junctions[(i + 1)..])
                {
                    double distance = current.Location.EuclidianDistance(other.Location);
                    distances.Add(new JunctionPair(distance, current, other));
                }
            }

            distances.Sort();

            return (junctions, distances);
        }

        private record JunctionBox(int Id, Point3D Location);

        private record JunctionPair(double Distance, JunctionBox Left, JunctionBox Right) : IComparable<JunctionPair>
        {
            /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
            /// <param name="other">An object to compare with this instance.</param>
            /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
            /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
            public int CompareTo(JunctionPair other) => this.Distance.CompareTo(other.Distance);
        }
    }
}
