﻿using System;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


namespace CodingSolution
{

    public interface IHasNumeric
    {
        double Num { get; }
    }


    public class SomeNumeric : IHasNumeric
    {
        public SomeNumeric(double d)
        {
            Num = d;
        }

        public double Num { get; set; }
    }

    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X;
        public double Y;
    }


    public class LibClass
    {

        public static double Approx(Point[] pts)
        {
            var pointsInsideCircleCount = 0;
            var totalPoints = pts.Count();

            foreach (var p in pts)
            {
                var xyScore = Math.Pow(p.X, 2) + Math.Pow(p.Y, 2);
                if (xyScore <= 1) // IN THE QUARTER
                {
                    pointsInsideCircleCount += 1;
                }
            }

            // chance of it being inside circle = PI/4
            // => chance of it being inside circle = PI/4
            // => (pointsInside(totalPoints)) = PI/4
            // => approximation of PI = (pointsInside(totalPoints)) * 4

            // the more points you process the more accurate the approximation should be

            var piApproximation = ((double)pointsInsideCircleCount / totalPoints) * 4;

            return piApproximation;
        }


        public static double Average(IEnumerable<IHasNumeric> set)
        {
            return set.Average(o => o.Num);
        }


        ///Given a number of square tiles. Place the tiles together into square shapes, making each square shape as large as possible, until all the tiles have been used.
        ///Return the number of tiles in each square.

        ///For example, 15 tiles = [9, 4, 1, 1]
        /// [ ][ ][ ]
        /// [ ][ ][ ]  [ ][ ]
        /// [ ][ ][ ]  [ ][ ]  [ ]  [ ]        

        public static IEnumerable<int> GetPanelArrays(int numPanels)
        {
            var numPanelsLeft = numPanels;
            var squareSizes = new List<int>();
            while (numPanelsLeft > 0)
            {
                var biggestSquare = (int)(Math.Pow(((int)Math.Floor(Math.Sqrt(numPanelsLeft))), 2));
                squareSizes.Add(biggestSquare);
                numPanelsLeft = numPanelsLeft - biggestSquare;
            }

            return squareSizes;
        }






        private int _counterClosure;
        private int _counterNonClosure;


        /// <summary>
        /// After executing this function, the value of _counter should be increased by n * (n + 1) / 2, however this is not always the case.
        /// Propose a change to the RunStep function to resolve the issue, whilst maintaining the asynchronous, concurrent execution behaviour.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public async Task<int> ParallelIncrement(int n)
        {
            _counterClosure = 0;
            _counterNonClosure = 0;


            var tasks = Enumerable.Range(1, n).Select(RunStep);
            await Task.WhenAll(tasks);


            var expectedCounter = (n * (n + 1) / 2);
            Console.WriteLine($"Expected {expectedCounter}, Closure {_counterClosure}, counterNONClosure {_counterNonClosure}");
            return _counterClosure;
        }

        private Task RunStep(int stepValue)
        {
            _counterNonClosure += stepValue;

            // move the captured variable outside the scope of the inner func
            // as it is being captured in a closure. I believe that there is no guarantee
            // as to when the Tasks actually get Run in a WhenAll(tasks) call. And the closure 
            // only gets created at runtime when the Func is being invoked so the captured
            // variable is not always the value we expect it should be when the capture occurs 
            // Could have the same value captured for consecutive tasks

            return Task.Run(async () =>
            {
                await Task.Delay(2);
                _counterClosure += stepValue;
            });
        }



        /// <summary>
        /// Return false if none of the requested capabilities have been granted, otherwise true.
        /// </summary>
        /// <param name="granted">Set of flags indicated which capabilities have been granted.</param>
        /// <param name="requested">Which capabilities are being requested.</param>
        /// <returns> Whether ANY of the required capabilities have been granted. Also return true if none is requested.
        /// </returns>
        public static bool AnyGranted(Capabilities granted, Capabilities requested)
        {
            /*
             * foreach flag in requested
             * 
             */

            bool hasBitSet = false;

            foreach (Capabilities flagToCheck in Enum.GetValues(typeof(Capabilities)))
            {
                if (requested.HasFlag(flagToCheck))
                {
                    if (granted.HasFlag(flagToCheck))
                    {
                        hasBitSet = true;
                    }
                }
            }

            return hasBitSet;

        }


    }


    [Flags]
    public enum Capabilities
    {
        None = 0,
        All = ~0,

        Spin = 1,
        Raise = 2,
        Lower = 4,
        Flatten = 8,
        Expand = 16,
        Drop = 32,
    }
}

