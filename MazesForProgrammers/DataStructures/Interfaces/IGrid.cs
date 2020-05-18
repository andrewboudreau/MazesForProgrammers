using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// An interface for traversing a 2-dimensional collection of <see cref="Cell"/>.
    /// </summary>
    public interface IGrid<out T> where T : Cell
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
        T RandomCell { get; }

        /// <summary>
        /// Gets the number of <see cref="Cell"/> with only 1 linked neighbor.
        /// </summary>
        IEnumerable<T> DeadEnds { get; }

        /// <summary>
        /// Gets an enumerable collection of <see cref="Cell"/> for each row.
        /// </summary>
        /// <returns>Returns an <see cref="IEnumerable{Cell}"/> for each row.</returns>
        IEnumerable<IEnumerable<T>> EachRow();

        /// <summary>
        /// Gets all of the cells.
        /// </summary>
        /// <returns>Returns every <see cref="Cell"/>.</returns>
        IEnumerable<T> EachCell();
    }
}