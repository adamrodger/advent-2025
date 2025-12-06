using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Tests
{
    public class Day6Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day6 solver;

        public Day6Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day6();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day6.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "123 328  51 64 ",
                " 45 64  387 23 ",
                "  6 98  215 314",
                "*   +   *   +  ",
            ];
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 4277556;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 5381996914800;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 6 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 3263827;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 9627174150897;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 6 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
