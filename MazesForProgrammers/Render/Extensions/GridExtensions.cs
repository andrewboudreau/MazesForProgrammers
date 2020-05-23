using System;
using System.Drawing;
using System.Linq;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Hex;
using MazesForProgrammers.DataStructures.Polar;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public static class GridExtensions
    {
        private static readonly Action<Graphics, PointF[], Cell> nodrawP = (x, y, z) => { };

        public static RectangleGrid RenderToConsole(this RectangleGrid grid, Distances distances)
        {
            return RenderToConsole(grid, cell => $" {Base36.Encode(distances[cell].GetValueOrDefault(-1))} ");
        }

        public static RectangleGrid RenderToConsole(this RectangleGrid grid, Func<Cell, string> cellRender = null)
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

        public static void DebugToConsole(this RectangleGrid grid)
        {
            var renderer = new CellDebugger();
            Console.WriteLine(renderer.Render(grid));
        }

        public static RectangleGrid RenderImageAndOpen(this RectangleGrid grid, int pixelsPerCell = 100)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);

            bitmap.SaveAndOpen("maze_{0}");
            return grid;
        }

        public static RectangleGrid RenderImageAndOpen(this RectangleGrid grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 30);

            bitmap.SaveAndOpen("maze_{0}_path");
            return grid;
        }

        public static RectangleGrid RenderImageAndOpen(this RectangleGrid grid, string file, int pixelsPerCell)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);
            bitmap.SaveAndOpen(file);

            return grid;
        }
        
        public static PolarGrid RenderImageAndOpen(this PolarGrid polarGrid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return polarGrid;
        }

        public static PolarGrid RenderImageAndOpen(this PolarGrid polarGrid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return polarGrid;
        }

        public static HexGrid RenderImageAndOpen(this HexGrid grid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return grid;
        }

        public static HexGrid RenderImageAndOpen(this HexGrid grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return grid;
        }

        public static TriangleGrid RenderImageAndOpen(this TriangleGrid grid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return grid;
        }

        public static TriangleGrid RenderImageAndOpen(this TriangleGrid grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return grid;
        }
    }
}