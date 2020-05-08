using System.Collections.Generic;
using System.Linq;
using MazesForProgrammers.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MazesForProgrammers.Tests.DataStructures
{
    [TestClass]
    public class IterationTests
    {
        [TestMethod]
        public void ForEachCell_IteratesRowsThenColumns()
        {
            var grid = new Grid(2);
            var seen = new List<Cell>(4);
            foreach (var cell in grid.EachCell())
            {
                seen.Add(cell);
            }

            Assert.AreEqual(4, seen.Count);

            Assert.AreEqual(seen[0].Row, 0);
            Assert.AreEqual(seen[0].Column, 0);

            Assert.AreEqual(seen[1].Row, 0);
            Assert.AreEqual(seen[1].Column, 1);

            Assert.AreEqual(seen[2].Row, 1);
            Assert.AreEqual(seen[2].Column, 0);

            Assert.AreEqual(seen[3].Row, 1);
            Assert.AreEqual(seen[3].Column, 1);
        }

        [TestMethod]
        public void ForEachRow_IteratesRowsThenColumns()
        {
            var grid = new Grid(2);
            var seen = new List<(int Row, List<Cell> Cells)>(4);
            
            var row = 0;
            foreach (var cells in grid.EachRow())
            {
                seen.Add((row, cells.ToList()));
                row++;
            }

            Assert.AreEqual(2, seen.Count);

            Assert.AreEqual(seen[0].Row, 0);
            Assert.AreEqual(seen[1].Row, 1);

            Assert.AreEqual(seen[0].Cells[0].Row, 0);
            Assert.AreEqual(seen[0].Cells[0].Column, 0);

            Assert.AreEqual(seen[0].Cells[1].Row, 0);
            Assert.AreEqual(seen[0].Cells[1].Column, 1);

            Assert.AreEqual(seen[1].Cells[0].Row, 1);
            Assert.AreEqual(seen[1].Cells[0].Column, 0);

            Assert.AreEqual(seen[1].Cells[1].Row, 1);
            Assert.AreEqual(seen[1].Cells[1].Column, 1);
        }
    }
}
