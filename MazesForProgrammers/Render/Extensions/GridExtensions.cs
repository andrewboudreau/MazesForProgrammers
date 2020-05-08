using MazesForProgrammers.DataStructures;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MazesForProgrammers.Render
{
    public static class GridExtensions
    {
        public static Grid RenderToConsole(this Grid grid)
        {
            var renderer = new ConsoleRender();
            Console.WriteLine(renderer.Render(grid));
            return grid;
        }

        public static void DebugToConsole(this Grid grid)
        {
            var renderer = new CellDebugger();
            Console.WriteLine(renderer.Render(grid));
        }

        public static Grid RenderToImage(this Grid grid, string outputFile)
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
