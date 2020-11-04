using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingSolution;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace VirtualRecall.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _console;
        private readonly IEnumerable<SomeNumeric> _countableSet;

        public Tests(ITestOutputHelper console)
        {
            _console = console;
            var set = new double[] { 4, 6, 1, 3, 7 };
            _countableSet = set.Select(s => new SomeNumeric(s));
        }

        [Fact]
        public void AveragingTests()
        {
            var averageResult = LibClass.Average(_countableSet);
            averageResult.Should().Be(4.2);
        }

        [Fact]
        public async Task ParallelIncrementingTest()
        {
            var lib = new LibClass();

            const int p = 1000;

            const int expected = p * (p + 1) / 2;


            var result = await lib.ParallelIncrement(p);
            result.Should().Be(expected);

            //And again...
            result = await lib.ParallelIncrement(p);
            result.Should().Be(expected * 2);
        }



        [Fact]
        public void ApproximatePiTest()
        {
            var rnd = new Random();

            var pts = Enumerable.Range(0, 1000000).Select(i => new Point(rnd.NextDouble(), rnd.NextDouble()));
            var approx = LibClass.Approx(pts.ToArray());

            const double errorMargin = 0.01;
            _console.WriteLine($"Approx: {approx}. Off by {approx - Math.PI}. AllowedMargin: {errorMargin}");

            approx.Should().BeInRange(Math.PI - errorMargin, Math.PI + errorMargin);

        }

        [Theory]
        [InlineData(15, new[] { 9, 4, 1, 1 })]
        [InlineData(20, new[] { 16, 4 })]
        public void ArraySizingTests(int numPanels, int[] expectedSizes)
        {
            LibClass.GetPanelArrays(numPanels).Should().BeEquivalentTo(expectedSizes);
        }


        [Theory]

        [InlineData(Capabilities.Spin, Capabilities.Flatten, false)]
        [InlineData(Capabilities.Spin | Capabilities.Flatten, Capabilities.Raise, false)]
        [InlineData(Capabilities.None, Capabilities.All, false)]
        [InlineData(Capabilities.None, Capabilities.Raise, false)]
        [InlineData(Capabilities.None, Capabilities.None, true)]
        [InlineData(Capabilities.Spin, Capabilities.None, true)]
        [InlineData(Capabilities.All, Capabilities.All, true)]
        [InlineData(Capabilities.Raise, Capabilities.Raise, true)]
        [InlineData(Capabilities.Spin | Capabilities.Flatten, Capabilities.Spin, true)]
        [InlineData(Capabilities.Spin, Capabilities.Flatten | Capabilities.Spin, true)]

        public void CapabilitiesTests(Capabilities granted, Capabilities toTest, bool expected)
        {
            LibClass.AnyGranted(granted, toTest).Should().Be(expected);
        }

    }
}
