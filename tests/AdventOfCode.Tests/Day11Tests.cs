using System.IO;
using Xunit;
using Xunit.Abstractions;


namespace AdventOfCode.Tests
{
    public class Day11Tests
    {
        private readonly ITestOutputHelper output;
        private readonly Day11 solver;

        public Day11Tests(ITestOutputHelper output)
        {
            this.output = output;
            this.solver = new Day11();
        }

        private static string[] GetRealInput()
        {
            string[] input = File.ReadAllLines("inputs/day11.txt");
            return input;
        }

        [Fact]
        public void Part1_SampleInput_ProducesCorrectResponse()
        {
            var expected = 5;

            var result = solver.Part1([
                "aaa: you hhh",
                "you: bbb ccc",
                "bbb: ddd eee",
                "ccc: ddd eee fff",
                "ddd: ggg",
                "eee: out",
                "fff: out",
                "ggg: out",
                "hhh: ccc fff iii",
                "iii: out"
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1_RealInput_ProducesCorrectResponse()
        {
            var expected = 758;

            var result = solver.Part1(GetRealInput());
            output.WriteLine($"Day 11 - Part 1 - {result}");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_SampleInput_ProducesCorrectResponse()
        {
            var expected = 2;

            var result = solver.Part2([
                "svr: aaa bbb",
                "aaa: fft",
                "fft: ccc",
                "bbb: tty",
                "tty: ccc",
                "ccc: ddd eee",
                "ddd: hub",
                "hub: fff",
                "eee: dac",
                "dac: fff",
                "fff: ggg hhh",
                "ggg: out",
                "hhh: out",
            ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2_RealInput_ProducesCorrectResponse()
        {
            var expected = 490695961032000;

            var result = solver.Part2(GetRealInput());
            output.WriteLine($"Day 11 - Part 2 - {result}");

            Assert.Equal(expected, result);
        }
    }
}
