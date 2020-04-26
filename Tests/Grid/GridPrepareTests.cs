using MazesForProgrammers.Grid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Tests.Grid
{
    [TestClass]
    public class GridPrepareTests
    {
        [TestMethod]
        public void ProvidesRowAndColumnInputs()
        {
            var seen = new List<(int, int)>();
            ICell<int> create(int row, int col)
            {
                seen.Add((row, col));
                return new Cell<int>(row, col);
            }

            var grid = new Grid<int>(2, 2, create);

            Assert.AreEqual(4, seen.Count);
            Assert.AreEqual(seen[0], (0, 0));
            Assert.AreEqual(seen[1], (0, 1));
            Assert.AreEqual(seen[2], (1, 0));
            Assert.AreEqual(seen[3], (1, 1));
        }

        [TestMethod]
        public void UsesProvidedCells()
        {
            var seen = new List<ICell<int>>();
            ICell<int> create(int row, int col)
            {
                var cell = new Cell<int>(row, col)
                {
                    Data = col * row + col
                };

                seen.Add(cell);
                return cell;
            }

            var grid = new Grid<int>(2, 2, create);

            Assert.AreEqual(4, seen.Count);

            var i = 0;
            foreach (var cell in grid.EachCell())
            {
                Assert.AreEqual(seen[i++], cell);
            }
        }
    }
}
