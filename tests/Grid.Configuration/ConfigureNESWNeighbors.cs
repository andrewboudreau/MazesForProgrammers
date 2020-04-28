using System.Linq;

using MazesForProgrammers.Grid;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.Grid.Configuration
{
    [TestClass]
    public class ConfigureCardinalDirectionNeighbors
    {
        [TestMethod]
        public void AllNeighborsAreDefined()
        {
            var grid = new Grid<int>(3);

            Assert.AreEqual(2, grid[(0, 0)].Neighbors.Count);
            Assert.AreEqual(3, grid[(0, 1)].Neighbors.Count);
            Assert.AreEqual(2, grid[(0, 2)].Neighbors.Count);

            CollectionAssert.AreEqual(grid[(0, 0)].Neighbors.ToList(), new[] { grid[(1, 0)], grid[(0, 1)] });
            CollectionAssert.AreEqual(grid[(0, 1)].Neighbors.ToList(), new[] { grid[(1, 1)], grid[(0, 2)], grid[(0, 0)] });
            CollectionAssert.AreEqual(grid[(0, 2)].Neighbors.ToList(), new[] { grid[(1, 2)], grid[(0, 1)] });

            Assert.AreEqual(3, grid[(1, 0)].Neighbors.Count);
            Assert.AreEqual(4, grid[(1, 1)].Neighbors.Count);
            Assert.AreEqual(3, grid[(1, 2)].Neighbors.Count);

            CollectionAssert.AreEqual(grid[(1, 0)].Neighbors.ToList(), new[] { grid[(2, 0)], grid[(1, 1)], grid[(0, 0)] });
            CollectionAssert.AreEqual(grid[(1, 1)].Neighbors.ToList(), new[] { grid[(2, 1)], grid[(1, 2)], grid[(0, 1)], grid[(1, 0)] });
            CollectionAssert.AreEqual(grid[(1, 2)].Neighbors.ToList(), new[] { grid[(2, 2)], grid[(0, 2)], grid[(1, 1)] });

            Assert.AreEqual(2, grid[(2, 0)].Neighbors.Count);
            Assert.AreEqual(3, grid[(2, 1)].Neighbors.Count);
            Assert.AreEqual(2, grid[(2, 2)].Neighbors.Count);

            CollectionAssert.AreEqual(grid[(2, 0)].Neighbors.ToList(), new[] { grid[(2, 1)], grid[(1, 0)] });
            CollectionAssert.AreEqual(grid[(2, 1)].Neighbors.ToList(), new[] { grid[(2, 2)], grid[(1, 1)], grid[(2, 0)] });
            CollectionAssert.AreEqual(grid[(2, 2)].Neighbors.ToList(), new[] { grid[(1, 2)], grid[(2, 1)] });
        }
    }
}
