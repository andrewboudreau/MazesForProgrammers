using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public interface ICell<T>
    {
        /// <summary>
        /// Gets the X-Coordinate of this cell in the grid.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Gets the Y-Coordinate of this cell in the grid.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Gets the X,Y Tuple of this cell's location in the grid.
        /// </summary>
        (int X, int Y) Location => (X, Y);

        /// <summary>
        /// Gets the cells data.
        /// </summary>
        T Data { get; }

        /// <summary>
        /// Gets a tuple of the x,y coordinates with the cell data.
        /// </summary>
        (int X, int Y, T Data) Item => (X, Y, Data);
        
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
