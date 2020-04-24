using MazesForProgrammers.Grid;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MazesForProgrammers.Tests.Cell
{
    [TestClass]
    public class CellLinksTests
    {
        [TestMethod]
        public void DefaultAddLink_AddsToBothCells()
        {
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other);

            CollectionAssert.Contains(cell.Links.ToList(), other);
            CollectionAssert.Contains(other.Links.ToList(), cell);
        }

        [TestMethod]
        public void UnidirectionalAdd_OnlyAddsToSelf()
        {
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other, false);

            CollectionAssert.Contains(cell.Links.ToList(), other);
            CollectionAssert.DoesNotContain(other.Links.ToList(), cell);
        }

        [TestMethod]
        public void DefaultRemoveLink_RemovesFromBothCells()
        {
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);
            
            cell.AddLink(other);
            CollectionAssert.Contains(cell.Links.ToList(), other);
            CollectionAssert.Contains(other.Links.ToList(), cell);

            cell.RemoveLink(other);
            CollectionAssert.DoesNotContain(cell.Links.ToList(), other);
            CollectionAssert.DoesNotContain(other.Links.ToList(), cell);
        }

        [TestMethod]
        public void UnidirectionalRemove_OnlyRemovesFromSelf()
        {
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other);
            CollectionAssert.Contains(cell.Links.ToList(), other);
            CollectionAssert.Contains(other.Links.ToList(), cell);

            cell.RemoveLink(other, false);
            CollectionAssert.DoesNotContain(cell.Links.ToList(), other);
            CollectionAssert.Contains(other.Links.ToList(), cell);
        }
        
        [TestMethod]
        public void LinkingSameCell_MaintainsSingleEntry()
        {
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other);
            cell.AddLink(other);

            Assert.AreEqual(cell.Links.Single(), other);
            Assert.AreEqual(other.Links.Single(), cell);
        }

        [TestMethod]
        public void RemoveLink_FromEmptyCollection()
        {
            // Arrange
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            // Act
            cell.RemoveLink(other);

            // Assert
            Assert.AreEqual(cell.Links.Any(), false, "The links collection should remain empty.");
            Assert.AreEqual(other.Links.Any(), false, "The links collection should remain empty.");
        }

        [TestMethod]
        public void RemoveLink_WhichDoesNotExists()
        {
            // Arrange
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);
            var missing = new Cell<int>(2, 2);

            cell.AddLink(other);

            // Act
            cell.RemoveLink(missing);
            
            // Assert
            CollectionAssert.Contains(cell.Links.ToList(), other);
            CollectionAssert.Contains(other.Links.ToList(), cell);

            CollectionAssert.DoesNotContain(cell.Links.ToList(), missing);
            CollectionAssert.DoesNotContain(other.Links.ToList(), missing);
            Assert.AreEqual(missing.Links.Any(), false, "The missing cell's link collection should remain empty.");
        }

        [TestMethod]
        public void Linked_ReturnsTrueAfterAddLink()
        {
            // Arrange
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other);

            // Assert
            Assert.IsTrue(cell.Linked(other));
            Assert.IsTrue(other.Linked(cell));
        }

        [TestMethod]
        public void Linked_ReturnsFalseAfterRemoveLink()
        {
            // Arrange
            var cell = new Cell<int>(0, 0);
            var other = new Cell<int>(1, 1);

            cell.AddLink(other);

            // Act
            cell.RemoveLink(other);

            Assert.IsFalse(cell.Linked(other));
            Assert.IsFalse(other.Linked(cell));
        }
    }
}
