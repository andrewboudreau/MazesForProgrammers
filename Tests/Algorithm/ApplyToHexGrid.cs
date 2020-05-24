using System;
using System.Drawing;
using System.Threading;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Hex;
using MazesForProgrammers.DataStructures.Polar;
using MazesForProgrammers.Render;
using MazesForProgrammers.Render.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.Algorithm
{
    [TestClass]
    /// <remarks>
    /// Apply all <see cref="IBuildMaze"/> algorithms to a Small, Medium, and Large <see cref="RectangleGrid"/>.
    /// </remarks>
    public class ApplyToHexGrid
    {
        [TestMethod]
        [DataRow("Small", 10, 50)]
        public void ApplyToSmallGrid(string size, int rows, int pixelsPerCell)
        {
            ApplyToGrid(size, rows, rows, pixelsPerCell);
        }

        [TestMethod]
        [DataRow("Medium", 50, 20)]
        public void ApplyToMediumGrid(string size, int rows, int pixelsPerCell)
        {
            ApplyToGrid(size, rows, rows, pixelsPerCell);
        }

        [TestMethod]
        [DataRow("Large", 100, 10)]
        public void ApplyToLargeGrid(string size, int rows, int pixelsPerCell)
        {
            ApplyToGrid(size, rows, rows, pixelsPerCell);
        }

        public void ApplyToGrid(string size, int rows, int columns, int pixelsPerCell)
        {
            void noDraw(Graphics a, PointF[] b, Cell c) { };

            var fileId = DateTime.UtcNow.Ticks.ToString();

            foreach (var algorithm in IBuildMazeExtensions.MazeBuilders())
            {
                var algo = algorithm.GetType().Name;
                Console.WriteLine($"Running {size} {algo}");
                var braid = new Braid(50);
                var grid = new HexGrid(rows, columns);
                algorithm.ApplyTo(grid);
                braid.ApplyTo(grid);
                var render = new ImageRender();

                using (var bitmap = render.Render(grid, grid[0, 0].Distances, pixelsPerCell))
                {
                    bitmap.SaveAndOpen($"{algo}_{size}_{fileId}_path");
                }

                using (var bitmap = render.Render(grid, noDraw, pixelsPerCell))
                {
                    bitmap.SaveAndOpen($"{algo}_{size}_{fileId}");
                }
            }
        }
    }
}
