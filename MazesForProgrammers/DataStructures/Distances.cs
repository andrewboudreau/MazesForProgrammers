using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.DataStructures
{
    public class Distances : IEnumerable<CellDistance>
    {
        private readonly Dictionary<Cell, int> cells;

        public Distances(Cell start, int value = 0)
        {
            cells = new Dictionary<Cell, int>
            {
                { start, value }
            };

            Max = new CellDistance(start, 0);
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
                if (cells.ContainsKey(cell) && value.GetValueOrDefault() > this[cell])
                {
                    throw new InvalidOperationException($"{cell} already has a distance '{this[cell]}' set. Cannot reassign to a higher '{value.GetValueOrDefault()}'.");
                }

                if (!value.HasValue)
                {
                    throw new InvalidOperationException($"{cell} distance cannot be set to `null`.");
                }

                if (cells.ContainsKey(cell))
                {
                    cells[cell] = value.Value;
                }
                else
                {
                    cells.Add(cell, value.Value);
                }
                
                if (value.Value > Max.Distance)
                {
                    Max = new CellDistance(cell, value.Value);
                }
            }
        }

        public CellDistance Max { get; private set; }

        public IEnumerator<CellDistance> GetEnumerator()
        {
            return cells.Select(ToCellDistance).GetEnumerator();
        }

        public float Intensity(Cell cell)
        {
            if (cell is null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            if (Max.Distance == 0)
            {
                return 0;
            }

            return (Max.Distance - this[cell].GetValueOrDefault()) / (float)Max.Distance;
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
