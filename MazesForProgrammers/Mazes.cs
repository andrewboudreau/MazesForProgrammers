using System;

using MazesForProgrammers.Grid;
using MazesForProgrammers.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MazesForProgrammers
{
    class Mazes
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static ILoggerFactory LogFactory;

        static void Main(string[] args)
        {
            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var logger = serviceProvider.GetRequiredService<ILogger<Mazes>>();

            var builder = serviceProvider.GetRequiredService<IMazeBuilder>();

            logger.LogCritical($"Maze: {builder.Build()}");


            var maze = new Grid<FourWalls>(5);
            foreach (var cell in maze)
            {

            }

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices(IServiceCollection services)
        {
            var singleLineConsoleLoggerProvider = new SingleLineConsoleLogger();
            services.AddLogging(configure => configure.ClearProviders().AddProvider(singleLineConsoleLoggerProvider).SetMinimumLevel(LoggingLevel));

            var serviceProvider = services.BuildServiceProvider();

            LogFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = serviceProvider.GetRequiredService<ILogger<Mazes>>();
            services.AddSingleton(typeof(ILogger), logger);

            return services.BuildServiceProvider();
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            return ConfigureServices(services);
        }
    }
}
