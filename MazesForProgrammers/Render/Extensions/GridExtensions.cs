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

        public static IGrid<RectangleCell> RenderToConsole(this IGrid<RectangleCell> grid, Distances distances)
        {
            return RenderToConsole(grid, cell => $" {Base36.Encode(distances[cell].GetValueOrDefault(-1))} ");
        }

        public static IGrid<RectangleCell> RenderToConsole(this IGrid<RectangleCell> grid, Func<Cell, string> cellRender = null)
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
            Console.WriteLine($"Found {grid.DeadEnds.Count():N0} dead ends.");
            return grid;
        }

        public static void DebugToConsole(this RectangleGrid grid)
        {
            var renderer = new ConsoleRender();
            Console.WriteLine(renderer.Debug(grid));
        }

        public static IGrid<RectangleCell> RenderImageAndOpen(this IGrid<RectangleCell> grid, int pixelsPerCell = 100)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);

            bitmap.SaveAndOpen("maze_{0}");
            return grid;
        }

        public static IGrid<RectangleCell> RenderImageAndOpen(this IGrid<RectangleCell> grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 30);

            bitmap.SaveAndOpen("maze_{0}_path");
            return grid;
        }

        public static IGrid<RectangleCell> RenderImageAndOpen(this IGrid<RectangleCell> grid, string file, int pixelsPerCell)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, pixelsPerCell);
            bitmap.SaveAndOpen(file);

            return grid;
        }
        
        public static IGrid<PolarCell> RenderImageAndOpen(this IGrid<PolarCell> polarGrid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return polarGrid;
        }

        public static IGrid<PolarCell> RenderImageAndOpen(this IGrid<PolarCell> polarGrid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(polarGrid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return polarGrid;
        }

        public static IGrid<HexCell> RenderImageAndOpen(this IGrid<HexCell> grid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return grid;
        }

        public static IGrid<HexCell> RenderImageAndOpen(this IGrid<HexCell> grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return grid;
        }

        public static IGrid<TriangleCell> RenderImageAndOpen(this IGrid<TriangleCell> grid)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, nodrawP, 20);
            bitmap.SaveAndOpen("maze_{0}");

            return grid;
        }

        public static IGrid<TriangleCell> RenderImageAndOpen(this IGrid<TriangleCell> grid, Distances distances)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid, distances, 50);
            bitmap.SaveAndOpen("maze_{0}_path");

            return grid;
        }
    }
}