using System;
using MazesForProgrammers.Grid;
using MazesForProgrammers.Grid.Render;
using MazesForProgrammers.Logging;
using MazesForProgrammers.Mazes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MazesForProgrammers
{
    class Program
    {
        public const LogLevel LoggingLevel = LogLevel.Debug;

        public static ILoggerFactory LogFactory;

        static void Main(string[] args)
        {
            using var scope = ConfigureServices().CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            var grid = new Grid<int>(3);
            var algorithm = new BinaryTree();
            var render = new ConsoleRender();

            algorithm
                .SetupNeighbors(grid)
                .ApplyTo(grid)
                .RenderToConsole();

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices(IServiceCollection services)
        {
            var singleLineConsoleLoggerProvider = new SingleLineConsoleLogger();
            services.AddLogging(configure => configure.ClearProviders().AddProvider(singleLineConsoleLoggerProvider).SetMinimumLevel(LoggingLevel));

            var serviceProvider = services.BuildServiceProvider();

            LogFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
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
