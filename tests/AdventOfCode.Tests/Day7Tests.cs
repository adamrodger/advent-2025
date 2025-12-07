using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day7Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day7 solver;

        public Day7Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day7();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day7.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                ".......S.......",
                "...............",
                ".......^.......",
                "...............",
                "......^.^......",
                "...............",
                ".....^.^.^.....",
                "...............",
                "....^.^...^....",
                "...............",
                "...^.^...^.^...",
                "...............",
                "..^...^.....^..",
                "...............",
                ".^.^.^.^.^...^.",
                "...............",
            ];
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 21;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 1579;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 7 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 40;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 13418215871354;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 7 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
