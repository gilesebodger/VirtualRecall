using CodingSolution;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static Random r = new Random();

        static async Task Main(string[] args)
        {
            var points = new List<CodingSolution.Point>();
            for (var i = 0; i <= 10000; i++)
            {
                points.Add(new CodingSolution.Point(r.NextDouble(), r.NextDouble()));
            }

            var pi = LibClass.Approx(points.ToArray());


            var numerics = new List<IHasNumeric>();
            for (var i = 1; i <= 10; i++)
            {
                numerics.Add(new SomeNumeric((double)i));
            }

            var average = LibClass.Average(numerics);
            var squares = LibClass.GetPanelArrays(39);
            var squares2 = LibClass.GetPanelArrays(41);
            var squares3 = LibClass.GetPanelArrays(47);
            var squares4 = LibClass.GetPanelArrays(54);
            var squares5 = LibClass.GetPanelArrays(15);
            var squares6 = LibClass.GetPanelArrays(374);
            var squares7 = LibClass.GetPanelArrays(600);

            var libClass = new LibClass();
            await libClass.ParallelIncrement(3);
            await libClass.ParallelIncrement(5);
            await libClass.ParallelIncrement(13);
            await libClass.ParallelIncrement(7);


            var granted = Capabilities.Expand | Capabilities.Raise | Capabilities.Spin;
            var requested = Capabilities.Flatten;
            bool anyGranted;
            if (LibClass.AnyGranted(granted, requested))
            {
                anyGranted = true;
            }
        }
    }
}
