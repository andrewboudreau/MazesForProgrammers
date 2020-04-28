using MazesForProgrammers.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            ApplicationLogging.LoggerFactory.CreateLogger("default");
        }
    }
}
