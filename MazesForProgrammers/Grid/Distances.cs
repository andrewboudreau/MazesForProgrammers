using System;
using System.Collections;
using System.Collections.Generic;

using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid
{
    public class Distances<T> : IEnumerable<ICell<T>>
    {
        private readonly Dictionary<ICell<T>, int> cells;

        public Distances(ICell<T> start)
        {
            cells = new Dictionary<ICell<T>, int>
            {
                { start, 0 }
            };
        }

        public int this[ICell<T> cell]
        {
            get { return cells[cell]; }
            set { cells.Add(cell, value); }
        }

        public IEnumerator<ICell<T>> GetEnumerator()
        {
            return cells.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
