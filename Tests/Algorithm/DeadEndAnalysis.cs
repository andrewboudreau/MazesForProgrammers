using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.Tests.Algorithm
{
    [TestClass]
    public class DeadEndAnalysis
    {
        [TestMethod]
        public void Analyze()
        {
            var tries = 100;
            var size = 20;
            var averages = new Dictionary<string, double>();

            foreach (var algorithm in IBuildMazeExtensions.MazeBuilders())
            {
                var algo = algorithm.GetType().Name;
                Console.WriteLine($"running {algo}");

                var deadends = new List<int>();
                for (var i = 0; i < tries; i++)
                {
                    var grid = new Grid(size);
                    algorithm.ApplyTo(grid);
                    deadends.Add(grid.DeadEnds.Count());
                }

                averages.Add(algo, deadends.Average());
            }

            Console.WriteLine();
            Console.WriteLine($"Average dead-ends per {size} by {size} maze ({size * size} cells):");

            foreach (var kvp in averages.OrderBy(x => x.Value))
            {
                var percent = averages[kvp.Key] * 100.0 / (size * size);
                Console.WriteLine($"{kvp.Key,-14}\t {averages[kvp.Key],-4:N0}/{size * size} \t({percent:N1}%)");
            }
        }
    }
}
