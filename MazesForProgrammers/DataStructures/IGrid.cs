using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// An interface for traversing a 2-dimensional collection of <see cref="Cell"/>.
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Gets the total number of <see cref="Cell"/>.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets a random <see cref="Cell"/>.
        /// </summary>
        Cell RandomCell { get; }

        /// <summary>
        /// Gets the Cell at given Row and Column.
        /// </summary>
        /// <param name="row">The 0-based row index.</param>
        /// <param name="column">The 0-based column index.</param>
        /// <returns>Returns null if the the coorindates are out of bounds.</returns>
        Cell this[int row, int column] { get; }

        /// <summary>
        /// Gets the number of <see cref="Cell"/> with only 1 linked neighbor.
        /// </summary>
        IEnumerable<Cell> DeadEnds { get; }

        /// <summary>
        /// Gets an enumerable collection of <see cref="Cell"/> for each row.
        /// </summary>
        /// <returns>Returns an <see cref="IEnumerable{Cell}"/> for each row.</returns>
        IEnumerable<IEnumerable<Cell>> EachRow();

        /// <summary>
        /// Gets all of the cells.
        /// </summary>
        /// <returns>Returns every <see cref="Cell"/>.</returns>
        IEnumerable<Cell> EachCell();
    }

    /// <summary>
    /// An generic interface for traversing a 2-dimensional collection of <see cref="Cell"/>.
    /// </summary>
    public interface IGrid<out T> : IGrid where T : Cell
    {
        /// <summary>
        /// Gets a random <see cref="Cell"/>.
        /// </summary>
        new T RandomCell { get; }

        /// <summary>
        /// Gets the Cell at given Row and Column.
        /// </summary>
        /// <param name="row">The 0-based row index.</param>
        /// <param name="column">The 0-based column index.</param>
        /// <returns>Returns null if the the coorindates are out of bounds.</returns>
        new T this[int row, int column] { get; }

        /// <summary>
        /// Gets the number of <see cref="Cell"/> with only 1 linked neighbor.
        /// </summary>
        new IEnumerable<T> DeadEnds { get; }

        /// <summary>
        /// Gets an enumerable collection of <see cref="Cell"/> for each row.
        /// </summary>
        /// <returns>Returns an <see cref="IEnumerable{Cell}"/> for each row.</returns>
        new IEnumerable<IEnumerable<T>> EachRow();

        /// <summary>
        /// Gets all of the cells.
        /// </summary>
        /// <returns>Returns every <see cref="Cell"/>.</returns>
        new IEnumerable<T> EachCell();
    }
}