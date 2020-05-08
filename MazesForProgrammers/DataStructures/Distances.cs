using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.DataStructures
{
    public class Distances : IEnumerable<CellDistance>
    {
        private readonly Dictionary<Cell, int> cells;

        public Distances(Cell start)
        {
            cells = new Dictionary<Cell, int>
            {
                { start, 0 }
            };
        }

        public int? this[Cell cell]
        {
            get
            {
                if (cell is null)
                {
                    return null;
                }

                if (cells.TryGetValue(cell, out var distance))
                {
                    return distance;
                }

                return null;
            }
            set
            {
                if (cells.ContainsKey(cell))
                {
                    throw new InvalidOperationException($"{cell} already has a distance '{cells[cell]}' set. Cannot reassign to '{value}'.");
                }

                if (!value.HasValue)
                {
                    throw new InvalidOperationException($"{cell} distance cannot be set to `null`.");
                }

                cells.Add(cell, value.Value);
            }
        }

        public void SetValue(Cell cell, int value)
        {
            cells[cell] = value;
        }

        public IEnumerator<CellDistance> GetEnumerator()
        {
            return cells.Select(ToCellDistance).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private CellDistance ToCellDistance(KeyValuePair<Cell, int> kvp)
        {
            return new CellDistance(kvp.Key, kvp.Value);
        }
    }

    public class CellDistance
    {
        public CellDistance(Cell cell, int distance)
        {
            Cell = cell;
            Distance = distance;
        }

        public Cell Cell { get; }

        public int Distance { get; }
    }
}
