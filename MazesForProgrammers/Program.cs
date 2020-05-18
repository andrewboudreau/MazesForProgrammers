﻿using System;
using System.Linq;
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
            //RandomSource.SetRandom(1);

            var grid = new PolarGrid(20);
            var algorithm = new RecursiveBacktracker();
            algorithm.ApplyTo(grid);

            var center = new Dijkstra(grid[0, 0]);
            var exit = center.Distances.Last(x => x.Cell.Row == grid.Rows - 1);
            grid.RenderImageAndOpen(center.PathToGoal(exit.Cell));
            Console.WriteLine($"Find the path from {grid[0, 0]} to {exit.Cell} ({exit.Distance} moves)");

            Console.ReadKey();
        }

        static void Masks()
        {
            var mask = Mask.FromImage(@"Masks\circle.png");

            var grid = new MaskedRectangleGrid(mask);
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
            var grid = new RectangleGrid(50);
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
