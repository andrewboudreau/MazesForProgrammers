using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Weighted;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Render;
using MazesForProgrammers.Render.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.Algorithm
{
    [TestClass]
    /// <remarks>
    /// Apply all <see cref="IBuildMaze"/> algorithms to a Small, Medium, and Large <see cref="RectangleGrid"/>.
    /// </remarks>
    public class WeightedMaze
    {
        [TestMethod]
        [DataRow("Small",4, 100)]
        public void ApplyToSmallGrid(string size, int rows, int pixelsPerCell)
        {
            ApplyToGrid(size, rows, rows, pixelsPerCell);
        }

        [TestMethod]
        [DataRow("Medium", 10, 20)]
        public void ApplyToMediumGrid(string size, int rows, int pixelsPerCell)
        {
            ApplyToGrid(size, rows, rows, pixelsPerCell);
        }

        //[TestMethod]
        //[DataRow("Large", 100, 10)]
        //public void ApplyToLargeGrid(string size, int rows, int pixelsPerCell)
        //{
        //    ApplyToGrid(size, rows, rows, pixelsPerCell);
        //}

        public void ApplyToGrid(string size, int rows, int columns, int pixelsPerCell)
        {
            RandomSource.SetRandom(4);
            var fileId = DateTime.UtcNow.Ticks.ToString();
            var render = new ImageRender();

            foreach (var algorithm in IBuildMazeExtensions.MazeBuilders().Skip(3).Take(1))
            {
                var algo = algorithm.GetType().Name;
                var braid = new Braid(50);
                Console.WriteLine($"Running {size} {algo}");

                var grid = new WeightedGrid(rows, columns);
                algorithm.ApplyTo(grid);
                braid.ApplyTo(grid);

                var start = grid[0, 0];
                var finish = grid[grid.Rows - 1, grid.Columns - 1];
                var shorestPath = new CostAwareDijkstra(start).PathToGoal(finish);

                using (var bitmap = render.Render(grid, shorestPath, pixelsPerCell))
                {
                    bitmap.SaveAndOpen($"{algo}_{size}_{fileId}_path");
                }

                // Lava flow, re-routing
                ((WeightedCell)(shorestPath.Where(x => x.Distance > 5).Sample().Cell)).Weight = 70;
                var rerouted = new CostAwareDijkstra(start).PathToGoal(finish);
                using (var bitmap = render.Render(grid, rerouted, pixelsPerCell))
                {
                    bitmap.SaveAndOpen($"{algo}_{size}_{fileId}_rerouted");
                }

                using (var bitmap = render.Render(grid, (a, b, c) => { }, pixelsPerCell))
                {
                    bitmap.SaveAndOpen($"{algo}_{size}_{fileId}");
                }
            }
        }
    }
}
