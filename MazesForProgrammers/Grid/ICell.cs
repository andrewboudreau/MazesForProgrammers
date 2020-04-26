using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    /// <summary>
    ///  A cell in the grid, aware of it's local neighbors as well as other linked cells.
    /// </summary>
    /// <typeparam name="T">The type of data to store in each cell.</typeparam>
    /// <remarks>These cells know about their location, can manage being linked to other cells and neighbors.</remarks>
    public interface ICell<T>
    {
        /// <summary>
        /// Gets the X-Coordinate of this cell in the grid.
        /// </summary>
        int Column { get; }

        /// <summary>
        /// Gets the Y-Coordinate of this cell in the grid.
        /// </summary>
        int Row { get; }

        /// <summary>
        /// Gets the X,Y Tuple of this cell's location in the grid.
        /// </summary>
        (int Row, int Column) Location => (Row, Column);

        /// <summary>
        /// Gets the cells data.
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Gets a tuple of the x,y coordinates with the cell data.
        /// </summary>
        (int Row, int Column, T Data) Item => (Row, Column, Data);

        /// <summary>
        /// Gets a tuple of the 4-way neighbors.
        /// </summary>
        (ICell<T> Top, ICell<T> Right, ICell<T> Bottom, ICell<T> Left) Neighbors { get; }

        /// <summary>
        /// Connects the current cell with the given cell.
        /// </summary>
        /// <param name="cell">The given cell.</param>
        /// <param name="bidirectional">If true the connection is recorded in both cells.</param>
        void AddLink(ICell<T> cell, bool bidirectional = true);

        /// <summary>
        /// Removes the connection from the current cell to the given cell.
        /// </summary>
        /// <param name="cell">The given cell.</param>
        /// <param name="bidirectional">If true the connection is removed in boths cells.</param>
        void RemoveLink(ICell<T> cell, bool bidirectional = true);

        /// <summary>
        /// Gets a collection of all the cells connected to this cell.
        /// </summary>
        IEnumerable<ICell<T>> Links { get; }

        /// <summary>
        /// Whether the current cell is linked to th another given cell.
        /// </summary>
        /// <param name="cell">The given cell</param>
        /// <returns>True if the given cell is in this cell's links collection.</returns>
        bool Find(ICell<T> cell);
    }
}
