using System.Collections.Generic;

using MazesForProgrammers.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.DataStructures
{
    [TestClass]
    public class GridPrepareTests
    {
        [TestMethod]
        public void ProvidesRowAndColumnInputs()
        {
            var seen = new List<(int, int)>();
            Cell create(int row, int col)
            {
                seen.Add((row, col));
                return new Cell(row, col);
            }

            var grid = new Grid(2, 2, create);

            Assert.AreEqual(4, seen.Count);
            Assert.AreEqual(seen[0], (0, 0));
            Assert.AreEqual(seen[1], (0, 1));
            Assert.AreEqual(seen[2], (1, 0));
            Assert.AreEqual(seen[3], (1, 1));
        }

        [TestMethod]
        public void UsesProvidedCells()
        {
            var seen = new List<Cell>();
            Cell create(int row, int col)
            {
                var cell = new Cell(row, col);

                seen.Add(cell);
                return cell;
            }

            var grid = new Grid(2, 2, create);

            Assert.AreEqual(4, seen.Count);

            var i = 0;
            foreach (var cell in grid.EachCell())
            {
                Assert.AreEqual(seen[i++], cell);
            }
        }
    }
}
