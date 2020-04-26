﻿using System.Collections.Generic;

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
        }

        public int Column { get; }

        public int Row { get; }

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

        public static ICell<TCell> DefaultCreate<TCell>(int row, int column)
        {
            return new Cell<TCell>(row, column);
        }
    }
}
