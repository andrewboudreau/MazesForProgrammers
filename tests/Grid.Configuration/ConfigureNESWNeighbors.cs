using MazesForProgrammers.Grid;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MazesForProgrammers.Tests.Grid.Configuration
{
    [TestClass]
    public class ConfigureCardinalDirectionNeighbors
    {
        [TestMethod]
        public void NeighborsAreDefined()
        {
            var grid = new Grid<int>(3);

            Assert.AreEqual(2, grid[(0, 0)].Neighbors.Count);
            Assert.AreEqual(3, grid[(0, 1)].Neighbors.Count);
            Assert.AreEqual(2, grid[(0, 2)].Neighbors.Count);

            Assert.AreEqual(3, grid[(1, 0)].Neighbors.Count);
            Assert.AreEqual(4, grid[(1, 1)].Neighbors.Count);
            Assert.AreEqual(3, grid[(1, 2)].Neighbors.Count);

            Assert.AreEqual(2, grid[(2, 0)].Neighbors.Count);
            Assert.AreEqual(3, grid[(2, 1)].Neighbors.Count);
            Assert.AreEqual(2, grid[(2, 2)].Neighbors.Count);
        }

        [TestMethod]
        public void AllNeighborsAreDefined()
        {
            var grid = new Grid<int>(3);
            CollectionAssert.AreEqual(grid[(1, 1)].Neighbors.ToList(), new[] { grid[(2, 1)], grid[(1, 2)], grid[(0, 1)], grid[(1, 0)] });
        }
    }
}
