using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MazesForProgrammers.DataStructures
{
    public class Distances : IEnumerable<CellDistance>
    {
        private readonly Dictionary<Cell, int> cells;
        private CellDistance max;

        public Distances(Cell start, int value = 0)
        {
            cells = new Dictionary<Cell, int>
            {
                { start, value }
            };

            max = new CellDistance(start, 0);
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
                    throw new InvalidOperationException($"{cell} already has a distance '{this[cell]}' set. Cannot reassign to '{value.GetValueOrDefault()}'.");
                }

                if (!value.HasValue)
                {
                    throw new InvalidOperationException($"{cell} distance cannot be set to `null`.");
                }

                cells.Add(cell, value.Value);
                if (value.Value > max.Distance)
                {
                    max = new CellDistance(cell, value.Value);
                }
            }
        }

        public CellDistance Max => max;

        public IEnumerator<CellDistance> GetEnumerator()
        {
            return cells.Select(ToCellDistance).GetEnumerator();
        }

        public Color BackgroundColor(Cell cell)
        {
            if (cell is null)
            {
                throw new ArgumentNullException(nameof(cell));
            }

            if (max.Distance == 0)
            {
                return Color.Transparent;
            }

            var intesity = (max.Distance - this[cell].GetValueOrDefault()) / (float)max.Distance;
            var dark = (int)(255 * intesity);
            var bright = (int)(128 + (127 * intesity));

            return Color.FromArgb(255, dark, bright, dark);
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
