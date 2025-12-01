using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day1Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day1 solver;

        public Day1Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day1();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day1.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "L68",
                "L30",
                "R48",
                "L5",
                "R60",
                "L55",
                "L1",
                "L99",
                "R14",
                "L82"
            ];
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 3;

            var result = solver.Part1(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 1011;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 1 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 6;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 5937;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 1 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("L49", 0)]
        [InlineData("L50", 1)]
        [InlineData("L149", 1)]
        [InlineData("L150", 2)]
        [InlineData("R49", 0)]
        [InlineData("R50", 1)]
        [InlineData("R149", 1)]
        [InlineData("R150", 2)]
        [InlineData("R49 R2", 1)]
        [InlineData("R49 R1", 1)]
        [InlineData("L49 L2", 1)]
        [InlineData("L49 L1", 1)]
        [InlineData("L50 R100", 2)]
        [InlineData("R50 L100", 2)]
        [InlineData("R50 L5", 1)]
        [InlineData("L50 R5", 1)]
        public void Part2_WhenCalled_IsCorrect(string instruction, int expected)
        {
            var result = solver.Part2(instruction.Split());

            Assert.Equal(expected, result);
        }
    }
}
