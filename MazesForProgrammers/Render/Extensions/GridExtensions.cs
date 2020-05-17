using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Polar;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public static class GridExtensions
    {
        public static Grid RenderToConsole(this Grid grid, Distances distances)
        {

            return RenderToConsole(grid, cell => $" {Base36.Encode(distances[cell].GetValueOrDefault(-1))} ");
        }

        public static Grid RenderToConsole(this Grid grid, Func<Cell, string> cellRender = null)
        {
            ConsoleRender renderer;
            if (cellRender is null)
            {
                renderer = new ConsoleRender();
            }
            else
            {
                renderer = new ConsoleRender(cellRender);
            }

            Console.WriteLine(renderer.Render(grid));
            Console.WriteLine($"Found {grid.DeadEnds.Count():N0} deadends.");
            return grid;
        }

        public static void DebugToConsole(this Grid grid)
        {
            var renderer = new CellDebugger();
            Console.WriteLine(renderer.Render(grid));
        }

        public static IGrid RenderImageAndOpen(this Grid grid, int pixelsPerCell = 100)
        {

            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);

            bitmap.SaveAndOpen("maze_{0}");
            return grid;
        }

        public static IGrid RenderImageAndOpen(this Grid grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances);

            bitmap.SaveAndOpen("maze_{0}_path");
            return grid;
        }

        public static IGrid RenderImageAndOpen(this Grid grid, string file, int pixelsPerCell)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);
            bitmap.SaveAndOpen(file);

            return grid;
        }

        public static IGrid RenderImageAndOpen(this PolarGrid polarGrid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return polarGrid;
        }

        public static IGrid RenderImageAndOpen(this PolarGrid polarGrid, Action<Graphics, PointF[], Cell> cellRenderer)
        {
            if (cellRenderer is null)
            {
                throw new ArgumentNullException(nameof(cellRenderer));
            }

            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, cellRenderer, 20);
            bitmap.SaveAndOpen("maze_{0}_path");

            return polarGrid;
        }
    }
}
