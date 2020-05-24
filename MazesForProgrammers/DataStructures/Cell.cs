using System;
using System.Collections.Generic;
using MazesForProgrammers.Algorithms;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// A node in the maze.
    /// </summary>
    public class Cell
    {
        private readonly HashSet<Cell> links;
        private readonly Lazy<Dijkstra> dijkstra;

        public Cell(int row, int column)
        {
            Column = column;
            Row = row;
            links = new HashSet<Cell>();
            dijkstra = new Lazy<Dijkstra>(() => new Dijkstra(this));
        }

        /// <summary>
        /// Gets the cells 0-based Column in the grid.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Gets the cells 0-based Row in the grid.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the cells which this cell is linked.
        /// </summary>
        public IEnumerable<Cell> Links => links;

        /// <summary>
        /// Gets the cells which share a border with this cell.
        /// </summary>
        public virtual IEnumerable<Cell> Neighbors => throw new NotImplementedException();

        /// <summary>
        /// Gets the distances to each other cell from this cell.
        /// </summary>
        /// <remarks>These distances are lazily generated upon first request.</remarks>
        public virtual Distances Distances
        {
            get
            {
                return dijkstra.Value.Distances;
            }
        }

        /// <summary>
        /// Gets the distances with a single path to the goal populated.
        /// </summary>
        /// <param name="goal">The goal cell.</param>
        /// <returns>A 2d array of integers with 's distances values populated</returns>
        public Distances PathTo(Cell goal)
        {
            return dijkstra.Value.PathToGoal(goal);
        }

        public bool Linked(Cell cell)
        {
            if (cell is null)
            {
                return false;
            }

            return links.Contains(cell);
        }

        public void AddLink(Cell cell, bool bidirectional = true)
        {
            if (cell is null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            links.Add(cell);
            if (bidirectional)
            {
                cell.AddLink(this, false);
            }
        }

        public void RemoveLink(Cell cell, bool bidirectional = true)
        {
            links.Remove(cell);
            if (bidirectional)
            {
                cell.RemoveLink(this, false);
            }
        }

        public override string ToString()
        {
            return $"[Row:{Row}, Col:{Column}]";
        }
    }
}
