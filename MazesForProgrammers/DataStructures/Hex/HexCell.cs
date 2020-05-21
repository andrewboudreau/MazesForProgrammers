using MazesForProgrammers.Extensions;
using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures.Hex
{
    public class HexCell : Cell
    {
        public HexCell(int row, int column)
            : base(row, column)
        {
        }

        public Cell NorthWest;
        public Cell North;
        public Cell NorthEast;
        public Cell SouthEast;
        public Cell South;
        public Cell SouthWest;

        public override IEnumerable<Cell> Neighbors => new Cell[] { NorthWest, North, NorthEast, SouthEast, South, SouthWest }.RemoveNulls();
    }
}
