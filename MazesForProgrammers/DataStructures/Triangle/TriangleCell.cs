using System.Collections.Generic;

using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures.Hex
{
    public class TriangleCell : Cell
    {
        public TriangleCell(int row, int column)
            : base(row, column)
        {
        }

        public Cell North;
        public Cell East;
        public Cell South;
        public Cell West;

        public bool Upright => (Row + Column) % 2 == 0;

        public override IEnumerable<Cell> Neighbors
        {
            get
            {
                var cells = new List<Cell>() { East, West };
                if (Upright && South is Cell)
                {
                    cells.Add(South);
                }

                if (!Upright && North is Cell)
                {
                    cells.Add(North);
                }

                return cells.RemoveNulls();
            }
        }
    }
}
