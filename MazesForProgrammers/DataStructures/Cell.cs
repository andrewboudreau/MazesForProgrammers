using System;
using System.Collections.Generic;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures
{
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

        public int Column { get; }
        public int Row { get; }

        public IEnumerable<Cell> Links => links;
        public virtual IEnumerable<Cell> Neighbors => new Cell[] { North, East, South, West }.RemoveNulls();

        public Cell North;
        public Cell East;
        public Cell South;
        public Cell West;

        public Distances Distances
        {
            get
            {
                return dijkstra.Value.Distances;
            }
        }

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
