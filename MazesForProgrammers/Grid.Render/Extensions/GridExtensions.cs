using System;
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
    }
}
