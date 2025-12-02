using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day2Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day2 solver;

        public Day2Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day2();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day2.txt");
            return input;
        }

        private static string[] GetSampleInput()
        {
            return
            [
                "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
            ];
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 8576933996;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 2 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 4_174_379_265;

            var result = solver.Part2(GetSampleInput());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 25663320831;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 2 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
