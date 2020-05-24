using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// Non-generic portion of see cref="IGrid"/> abstract implementation.
    /// </summary>
    public abstract partial class AbstractGrid<T> : IGrid<T> where T : Cell
    {
        /// <inheritdoc />
        IEnumerable<IEnumerable<Cell>> IGrid.EachRow()
        {
            return EachRow().Cast<IEnumerable<Cell>>();
        }

        /// <inheritdoc />
        IEnumerable<Cell> IGrid.EachCell()
        {
            return EachCell().Cast<Cell>();
        }

        /// <inheritdoc />
        Cell IGrid.RandomCell => RandomCell;

        /// <inheritdoc />
        IEnumerable<Cell> IGrid.DeadEnds => DeadEnds;

        /// <inheritdoc />
        Cell IGrid.this[int row, int column] => this[row, column];
    }
}