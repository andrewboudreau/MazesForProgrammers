using System.Linq;
using MazesForProgrammers.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.Configuration
{
    [TestClass]
    public class ConfigureCardinalDirectionNeighbors
    {
        [TestMethod]
        public void AllNeighborsAreDefined()
        {
            var grid = new Grid(3);

            Assert.AreEqual(2, grid[0, 0].Neighbors.Count());
            Assert.AreEqual(3, grid[0, 1].Neighbors.Count());
            Assert.AreEqual(2, grid[0, 2].Neighbors.Count());

            CollectionAssert.AreEquivalent(grid[0, 0].Neighbors.ToList(), new[] { grid[0, 0].South, grid[0, 0].East });
            CollectionAssert.AreEquivalent(grid[0, 1].Neighbors.ToList(), new[] { grid[0, 1].South, grid[0, 1].East, grid[0, 1].West });
            CollectionAssert.AreEquivalent(grid[0, 2].Neighbors.ToList(), new[] { grid[0, 2].South, grid[0, 2].West });
           
            Assert.AreEqual(3, grid[1, 0].Neighbors.Count());
            Assert.AreEqual(4, grid[1, 1].Neighbors.Count());
            Assert.AreEqual(3, grid[1, 2].Neighbors.Count());

            CollectionAssert.AreEquivalent(grid[1, 0].Neighbors.ToList(), new[] { grid[1, 0].North, grid[1, 0].East, grid[1, 0].South });
            CollectionAssert.AreEquivalent(grid[1, 1].Neighbors.ToList(), new[] { grid[1, 1].North, grid[1, 1].South, grid[1, 1].East, grid[1, 1].West });
            CollectionAssert.AreEquivalent(grid[1, 2].Neighbors.ToList(), new[] { grid[1, 2].North, grid[1, 2].South, grid[1, 2].West});

            Assert.AreEqual(2, grid[2, 0].Neighbors.Count());
            Assert.AreEqual(3, grid[2, 1].Neighbors.Count());
            Assert.AreEqual(2, grid[2, 2].Neighbors.Count());

            CollectionAssert.AreEquivalent(grid[2, 0].Neighbors.ToList(), new[] { grid[2, 0].North, grid[2, 0].East });
            CollectionAssert.AreEquivalent(grid[2, 1].Neighbors.ToList(), new[] { grid[2, 1].North, grid[2, 1].East, grid[2, 1].West });
            CollectionAssert.AreEquivalent(grid[2, 2].Neighbors.ToList(), new[] { grid[2, 2].North, grid[2, 2].West});
        }
    }
}
