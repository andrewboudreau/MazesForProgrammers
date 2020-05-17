using MazesForProgrammers.Extensions;
using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures.Polar
{
    public class PolarCell : Cell
    {
        public PolarCell(int row, int column)
            : base(row, column)
        {
            Outward = new List<Cell>();
        }

        public override IEnumerable<Cell> Neighbors
        {
            get
            {
                var neighbors = new List<Cell>(3 + Outward.Count)
                {
                    Clockwise,
                    CounterClockwise,
                    Inward
                };

                neighbors.AddRange(Outward);
                return neighbors.RemoveNulls();
            }
        }

        public Cell Clockwise;
        public Cell CounterClockwise;
        public Cell Inward;
        public List<Cell> Outward;
    }
}
