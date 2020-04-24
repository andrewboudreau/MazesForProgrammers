using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public class Cell<T> : ICell<T>
    {
        private readonly HashSet<ICell<T>> links;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            links = new HashSet<ICell<T>>(X * Y);
        }

        public int X { get; }

        public int Y { get; }

        public T Data { get; set; }

        public (ICell<T> Top, ICell<T> Right, ICell<T> Bottom, ICell<T> Left) Neighbors { get; set; }

        public IEnumerable<ICell<T>> Links => links;

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

        public bool Find(ICell<T> cell)
        {
            return links.Contains(cell);
        }

        
    }
}
