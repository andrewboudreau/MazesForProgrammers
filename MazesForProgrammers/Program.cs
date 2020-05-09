using System;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Logging;
using MazesForProgrammers.Mazes;
using MazesForProgrammers.Render;
using Microsoft.Extensions.Logging;

namespace MazesForProgrammers
{
    class Program
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static ILoggerFactory LogFactory = ApplicationLogging.LoggerFactory;

        static void Main(string[] args)
        {
            Grid.SetRandom();
            var grid = new Grid(9);
            var algorithm = new SideWinder();

            algorithm.ApplyTo(grid);

            var pass1 = new Dijkstra(grid[0, 0]);
            var start = pass1.Max.Cell;

            var pass2 = new Dijkstra(start);
            var goal = pass2.Max.Cell;

            grid
                .RenderToConsole(start.PathTo(goal))
                .RenderToImage($"output_{DateTime.Now.Ticks}.png", pass2.Distances);

            Console.ReadKey();
        }

        static void Main2(string[] args)
        {
            Grid.SetRandom();
            var grid = new Grid(9);
            var algorithm = new SideWinder();

            for (var i = 0; i < 1; i++)
            {
                algorithm
                    .ApplyTo(grid)
                    .RenderToConsole(grid[0, 0].PathTo(grid[8, 8]))
                    .RenderToImage($"output_{DateTime.Now.Ticks}.png", null);

                // grid.DebugToConsole();
            }

            Console.ReadKey();
        }
    }
}
