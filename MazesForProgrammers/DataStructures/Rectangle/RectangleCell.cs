using System.Collections.Generic;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures
{
    public class RectangleCell : Cell
    {
        public RectangleCell(int row, int column)
            : base(row, column)
        {
        }

        public Cell North;
        public Cell East;
        public Cell South;
        public Cell West;

        /// <summary>
        /// Gets the cells which share a border with this cell.
        /// </summary>
        public override IEnumerable<Cell> Neighbors => new Cell[] { North, East, South, West }.RemoveNulls();
    }
}