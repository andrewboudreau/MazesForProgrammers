using System;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Mazes;
using MazesForProgrammers.Render;

namespace MazesForProgrammers
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid.SetRandom();
            var grid = new Grid(20);
            var algorithm = new Wilson();
            algorithm.ApplyTo(grid);

            // var middle = grid[grid.Rows / 2, grid.Columns / 2];
            var start = grid[0, 0];

            grid
                .RenderToConsole(start.Distances)
                .RenderToImage($"output_{DateTime.Now.Ticks}.png", start.Distances);

            Console.ReadKey();
        }

        private static void LongestPathDemo()
        {
            Grid.SetRandom();
            var grid = new Grid(50);
            var algorithm = new SideWinder();

            algorithm.ApplyTo(grid);

            var pass1 = new Dijkstra(grid[0, 0]);
            var start = pass1.Max.Cell;

            var pass2 = new Dijkstra(start);
            var goal = pass2.Max.Cell;

            grid
                .RenderToConsole(start.PathTo(goal))
                .RenderToImage($"output_{DateTime.Now.Ticks}.png", pass2.Distances);
        }
    }
}
