using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public interface IGrid<T>
    {
        /// <summary>
        /// Gets the number of columns in the grid.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Gets the number of cells in the grid.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 0-based row and column tuple to read a cell in the grid.
        /// </summary>
        /// <param name="location">The 0-based row and column index.</param>
        /// <returns>A cell in the grid at the given row and column.</returns>
        ICell<T> this[(int Row, int Column) location] { get; }

        /// <summary>
        /// Gets a random cell from the grid.
        /// </summary>
        ICell<T> RandomCell { get; }

        /// <summary>
        /// Provides enumeration of each cell in each row.
        /// </summary>
        /// <returns>All cells in a grid.</returns>
        IEnumerable<ICell<T>> EachCell();

        /// <summary>
        /// Iterator for each row of the grid.
        /// </summary>
        /// <returns>Returns a collection of tuples which contains the row number and the cells for that row.</returns>
        IEnumerable<(int Row, IEnumerable<ICell<T>> Cells)> EachRow();

        /// <summary>
        /// Checks if the row and coloumn location is within the bounds of the current grid.
        /// </summary>
        /// <param name="row">0-based row and column index.</param>
        /// <returns>True if the row and column indexes are valid for the grid.</returns>
        bool InBounds((int Row, int Column) location);
    }
}