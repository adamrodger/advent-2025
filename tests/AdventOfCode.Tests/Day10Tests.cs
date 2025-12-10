using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day10Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day10 solver;

        public Day10Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day10();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day10.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
                "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
                "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
            ];
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 419;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 10 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 33;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 18369;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 10 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
