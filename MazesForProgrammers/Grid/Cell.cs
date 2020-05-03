using System.Collections.Generic;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid
{
    public class Cell<T> : ICell<T>
    {
        private readonly HashSet<ICell<T>> links;

        public Cell(int row, int column)
        {
            Column = column;
            Row = row;
            links = new HashSet<ICell<T>>(Column * Row);
            Neighbors = new List<ICell<T>>(4);
            Distances = new Distances<ICell<T>>(this);
        }

        public int Column { get; }

        public int Row { get; }

        public T Data { get; set; }

        public ICollection<ICell<T>> Neighbors { get; set; }

        public IEnumerable<ICell<T>> Links => links;

        public Distances<ICell<T>> Distances { get; set; }

        public bool Linked(ICell<T> cell)
        {
            return links.Contains(cell);
        }

        public void AddLink(ICell<T> cell, bool bidirectional = true)
        {
            links.Add(cell);
            if (bidirectional)
            {
                cell.AddLink(this, false);
            }
        }

        public void RemoveLink(ICell<T> cell, bool bidirectional = true)
        {
            links.Remove(cell);
            if (bidirectional)
            {
                cell.RemoveLink(this, false);
            }
        }

        public void Clear()
        {
            links.Clear();
            Neighbors.Clear();
        }

        public override string ToString()
        {
            return $"[Row:{Row}, Col:{Column}]";
        }
    }
}
