using Microsoft.Extensions.Logging;

namespace MazesForProgrammers.Logging
{
    public class ApplicationLogging
    {
        private static ILoggerFactory Factory = null;

        public static void ConfigureLogger(ILoggerFactory factory)
        {
            var singleLineConsoleLoggerProvider = new SingleLineConsoleLogger();
            factory.AddProvider(singleLineConsoleLoggerProvider);
        }

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (Factory == null)
                {
                    Factory = new LoggerFactory();
                    ConfigureLogger(Factory);
                }
                return Factory;
            }
            set { Factory = value; }
        }

        public static ILogger CreateLogger() => LoggerFactory.CreateLogger("default");
    }
}
