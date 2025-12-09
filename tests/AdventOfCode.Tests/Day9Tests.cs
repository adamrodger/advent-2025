using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day9Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day9 solver;

        public Day9Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day9();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day9.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "7,1",
                "11,1",
                "11,7",
                "9,7",
                "9,5",
                "2,5",
                "2,3",
                "7,3"
            ];
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 50;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 4754955192;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 9 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 24;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 1568849600;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 9 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
