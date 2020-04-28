using System;

using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Render
{
    public static class GridExtensions
    {
        public static void RenderToConsole<T>(this IGrid<T> grid)
        {
            var renderer = new ConsoleRender();
            Console.WriteLine(renderer.Render(grid));
        }
    }
}
