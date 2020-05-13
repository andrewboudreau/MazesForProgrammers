using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

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

        public static Grid RenderToImage(this Grid grid, string outputFile, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances);
            bitmap.Save(outputFile, ImageFormat.Png);

            var path = System.IO.Path.GetFullPath(outputFile);
            Process.Start(@"cmd.exe ", $@"/c {path}");

            return grid;
        }

        public static Grid RenderToImage(this Grid grid, string outputFile, int pixelsPerCell = 100)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, (gfx, rect, cell) => { }, pixelsPerCell);
            bitmap.Save(outputFile, ImageFormat.Png);

            var path = System.IO.Path.GetFullPath(outputFile);
            Process.Start(@"cmd.exe ", $@"/c {path}");

            return grid;
        }
    }
}
