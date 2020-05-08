using System.Collections;
using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures
{
    public class Distances : IEnumerable<Cell>
    {
        private readonly Dictionary<Cell, int> cells;

        public Distances(Cell start)
        {
            cells = new Dictionary<Cell, int>
            {
                { start, 0 }
            };
        }

        public int this[Cell cell]
        {
            get { return cells[cell]; }
            set { cells.Add(cell, value); }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return cells.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
