using System;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Polar;
using MazesForProgrammers.Mazes;
using MazesForProgrammers.Render;

namespace MazesForProgrammers
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid.SetRandom(1);

            var grid = new PolarGrid(50);
            var algorithm = new RecursiveBacktracker();
            algorithm.ApplyTo(grid);

            grid.RenderImageAndOpen(grid.RandomCell.Distances);

            Console.ReadKey();
        }

        static void Masks()
        {
            Grid.SetRandom();
            var mask = Mask.FromImage(@"Masks\circle.png");

            var grid = new MaskedGrid(mask);
            var algorithm = new RecursiveBacktracker();
            algorithm.ApplyTo(grid);

            var start = grid.RandomCell;
            //grid
            //    //.RenderToConsole(start.Distances)
            //    .RenderImageAndOpen(start.Distances)
            //    .RenderImageAndOpen($"maze_{DateTime.Now.Ticks}.png");

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
                .RenderImageAndOpen(pass2.Distances);
        }
    }
}
