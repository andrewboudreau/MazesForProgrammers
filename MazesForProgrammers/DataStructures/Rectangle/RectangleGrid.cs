using System;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// A rectangular grid of <see cref="Cell"/>.
    /// </summary>
    public class RectangleGrid : AbstractGrid<RectangleCell>
    {
        public RectangleGrid(int dimension = 3)
            : this(dimension, dimension, (row, col) => new RectangleCell(row, col))
        {
        }

        public RectangleGrid(int rows, int columns, Func<int, int, RectangleCell> create)
            : base(rows, columns, create)
        {
        }

        protected override void ConfigureNeighbors()
        {
            foreach (var cell in EachCell())
            {
                if (cell is null)
                {
                    return;
                }

                cell.North = this[cell.Row - 1, cell.Column];
                cell.South = this[cell.Row + 1, cell.Column];

                cell.East = this[cell.Row, cell.Column + 1];
                cell.West = this[cell.Row, cell.Column - 1];
            }
        }
    }
}