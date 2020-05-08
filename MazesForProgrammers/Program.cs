using System;
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
            var grid = new Grid(3);
            var algorithm = new SideWinder();

            for (var i = 0; i < 1; i++)
            {
                algorithm
                    .ApplyTo(grid)
                    .RenderToConsole()
                    .RenderToImage($"output_{DateTime.Now.Ticks}.png");

                grid.DebugToConsole();
            }

            Console.ReadKey();
        }
    }
}
