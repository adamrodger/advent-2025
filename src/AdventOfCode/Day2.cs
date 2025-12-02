using System;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 2
    /// </summary>
    public class Day2
    {
        public long Part1(string[] input)
        {
            long total = 0;

            foreach (string range in input[0].Split(','))
            {
                long[] bounds = range.Split('-').Select(long.Parse).ToArray();

                for (long i = bounds[0]; i <= bounds[1]; i++)
                {
                    string numString = i.ToString();

                    if (HasEqualParts(numString, 2))
                    {
                        total += i;
                    }
                }
            }

            return total;
        }

        public long Part2(string[] input)
        {
            long total = 0;

            foreach (string range in input[0].Split(','))
            {
                long[] bounds = range.Split('-').Select(long.Parse).ToArray();

                for (long i = bounds[0]; i <= bounds[1]; i++)
                {
                    string numString = i.ToString();

                    bool repeats = Enumerable.Range(2, numString.Length - 1)
                                             .Any(parts => HasEqualParts(numString, parts));

                    if (repeats)
                    {
                        total += i;
                    }
                }
            }

            return total;
        }

        /// <summary>
        /// Split the input string into the given number of equally sized parts and
        /// check if all the parts are equal
        /// </summary>
        /// <param name="input">Input to check</param>
        /// <param name="numParts">Number of parts for chunking the string</param>
        /// <returns>Input divides into numParts equal parts</returns>
        private static bool HasEqualParts(string input, int numParts)
        {
            (int chunkSize, int remainder) = Math.DivRem(input.Length, numParts);

            if (remainder != 0)
            {
                // doesn't evenly split
                return false;
            }

            int i = chunkSize;
            string first = input[..chunkSize];

            while (i < input.Length)
            {
                string chunk = input[i..(i + chunkSize)];

                if (chunk != first)
                {
                    // at least one non-matching chunk
                    return false;
                }

                i += chunkSize;
            }

            return true;
        }
    }
}
