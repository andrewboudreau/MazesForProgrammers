using System.Collections.Generic;

using MazesForProgrammers.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.DataStructures
{
    [TestClass]
    public class RandomCellTests
    {
        [TestMethod]
        public void ReturnsCell()
        {
            var seen = new HashSet<Cell>();
            var grid = new RectangleGrid(2);

            for (var i = 0; i < 100_000; i++)
            {
                seen.Add(grid.RandomCell);
            }

            Assert.AreEqual(4, seen.Count);
        }
    }
}
