using MazesForProgrammers.Grid;
using MazesForProgrammers.Tests.Cell;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MazesForProgrammers.Tests.Grid
{
    [TestClass]
    public class RandomCellTests
    {
        [TestMethod]
        public void ReturnsCell()
        {
            var seen = new HashSet<ICell<int>>();
            var grid = new Grid<int>(2);

            for (var i = 0; i < 100_000; i++)
            {
                seen.Add(grid.RandomCell);
            }

            Assert.AreEqual(4, seen.Count);
        }
    }
}
