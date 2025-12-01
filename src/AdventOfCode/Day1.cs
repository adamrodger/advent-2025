using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 1
    /// </summary>
    public class Day1
    {
        public int Part1(string[] input)
        {
            int current = 50;
            int answer = 0;

            foreach (string line in input)
            {
                int n = line.Numbers<int>().First();

                current += line[0] == 'R' ? n : -n;
                current %= 100;

                if (current == 0)
                {
                    answer++;
                }
            }

            return answer;
        }

        public int Part2(string[] input)
        {
            int current = 50;
            int answer = 0;

            foreach (string line in input)
            {
                int n = line.Numbers<int>().First();

                // cut down to less than one full rotation (if > 100)
                (int fullCycles, int remainder) = int.DivRem(n, 100);
                answer += fullCycles;

                // simulate the remaining moves
                for (int i = 0; i < remainder; i++)
                {
                    current += line[0] == 'R' ? 1 : -1;
                    current %= 100;

                    if (current == 0)
                    {
                        answer++;
                    }
                }
            }

            return answer;
        }
    }
}
