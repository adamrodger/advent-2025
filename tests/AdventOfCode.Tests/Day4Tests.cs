using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day4Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day4 solver;

        public Day4Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day4();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day4.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "..@@.@@@@.",
                "@@@.@.@.@@",
                "@@@@@.@.@@",
                "@.@@@@..@.",
                "@@.@@@@.@@",
                ".@@@@@@@.@",
                ".@.@.@.@@@",
                "@.@@@.@@@@",
                ".@@@@@@@@.",
                "@.@.@@@.@."
            ];
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 13;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 1457;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 4 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 43;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 8310;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 4 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
