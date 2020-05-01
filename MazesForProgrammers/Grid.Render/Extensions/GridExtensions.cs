using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Render
{
    public static class GridExtensions
    {
        public static IGrid<T> RenderToConsole<T>(this IGrid<T> grid)
        {
            var renderer = new ConsoleRender();
            Console.WriteLine(renderer.Render(grid));
            return grid;
        }

        public static void DebugToConsole<T>(this IGrid<T> grid)
        {
            var renderer = new CellDebugger();
            Console.WriteLine(renderer.Render(grid));
        }

        public static IGrid<T> RenderToImage<T>(this IGrid<T> grid, string outputFile)
        {
            var renderer = new ImageRender();
            using var bitmap = renderer.Render(grid);
            bitmap.Save(outputFile, ImageFormat.Png);

            var path = System.IO.Path.GetFullPath(outputFile);
            Process.Start(@"cmd.exe ", $@"/c {path}");

            return grid;
        }
    }
}
