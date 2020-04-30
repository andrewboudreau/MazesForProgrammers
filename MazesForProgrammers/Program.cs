using System;
using MazesForProgrammers.Grid;
using MazesForProgrammers.Grid.Render;
using MazesForProgrammers.Logging;
using MazesForProgrammers.Mazes;

using Microsoft.Extensions.Logging;

namespace MazesForProgrammers
{
    class Program
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static ILoggerFactory LogFactory = ApplicationLogging.LoggerFactory;

        static void Main(string[] args)
        {
            var grid = new Grid<int>(20);
            var algorithm = new SideWinder();

            for (var i = 0; i < 1; i++)
            {
                algorithm
                    .SetupNeighbors(grid)
                    .ApplyTo(grid)
                    .RenderToConsole()
                    .ClearLinksAndNeighbors();
            }

            Console.ReadKey();
        }
    }
}
