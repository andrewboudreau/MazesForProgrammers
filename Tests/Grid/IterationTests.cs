using MazesForProgrammers.Grid;
using MazesForProgrammers.Grid.Interfaces;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazesForProgrammers.Tests.Grid
{
    [TestClass]
    public class IterationTests
    {
        [TestMethod]
        public void ForEachCell_IteratesRowsThenColumns()
        {
            var grid = new Grid<int>(2);
            var seen = new List<ICell<int>>(4);
            foreach (var cell in grid.EachCell())
            {
                seen.Add(cell);
            }

            Assert.AreEqual(4, seen.Count);

            Assert.AreEqual(seen[0].Row, 0);
            Assert.AreEqual(seen[0].Column, 0);
            Assert.AreEqual(seen[0].Data, 0);

            Assert.AreEqual(seen[1].Row, 0);
            Assert.AreEqual(seen[1].Column, 1);
            Assert.AreEqual(seen[1].Data, 0);

            Assert.AreEqual(seen[2].Row, 1);
            Assert.AreEqual(seen[2].Column, 0);
            Assert.AreEqual(seen[2].Data, 0);

            Assert.AreEqual(seen[3].Row, 1);
            Assert.AreEqual(seen[3].Column, 1);
            Assert.AreEqual(seen[3].Data, 0);
        }

        [TestMethod]
        public void ForEachRow_IteratesRowsThenColumns()
        {
            var grid = new Grid<int>(2);
            var seen = new List<(int Row, List<ICell<int>> Cells)>(4);

            foreach (var (Row, Cells) in grid.EachRow())
            {
                seen.Add((Row, Cells.ToList()));
            }

            Assert.AreEqual(2, seen.Count);

            Assert.AreEqual(seen[0].Row, 0);
            Assert.AreEqual(seen[1].Row, 1);

            Assert.AreEqual(seen[0].Cells[0].Row, 0);
            Assert.AreEqual(seen[0].Cells[0].Column, 0);
            Assert.AreEqual(seen[0].Cells[0].Data, 0);

            Assert.AreEqual(seen[0].Cells[1].Row, 0);
            Assert.AreEqual(seen[0].Cells[1].Column, 1);
            Assert.AreEqual(seen[0].Cells[1].Data, 0);

            Assert.AreEqual(seen[1].Cells[0].Row, 1);
            Assert.AreEqual(seen[1].Cells[0].Column, 0);
            Assert.AreEqual(seen[1].Cells[0].Data, 0);

            Assert.AreEqual(seen[1].Cells[1].Row, 1);
            Assert.AreEqual(seen[1].Cells[1].Column, 1);
            Assert.AreEqual(seen[1].Cells[1].Data, 0);
        }
    }
}
