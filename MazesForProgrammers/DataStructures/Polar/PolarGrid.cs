using System;
using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures.Polar
{
    public class PolarGrid : IGrid
    {
        public static Random Random = new Random(0);

        public static void SetRandom(int? seed = null)
        {
            if (seed.HasValue)
            {
                Random = new Random(seed.Value);
            }
            else
            {
                Random = new Random();
            }
        }

        private List<PolarCell>[] rows;

        public PolarGrid(int rows = 6)
            : this(rows, (row, col) => new PolarCell(row, col))
        {
        }

        public PolarGrid(int rows, Func<int, int, PolarCell> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            if (rows < 2)
            {
                throw new ArgumentOutOfRangeException("Polar Grid must contain 2 or more rows.");
            }

            Rows = rows;

            this.rows = Prepare(create);
            ConfigureNeighbors();
        }

        public int Rows { get; protected set; }

        public int Columns
        {
            get
            {
                throw new NotImplementedException($"The number of columns varies for {nameof(PolarGrid)} and is not a single value.");
            }
        }

        public int Size => EachRow().Sum(x => x.Count());

        public IEnumerable<Cell> DeadEnds => EachCell().RemoveNulls().Where(x => x.Links.Count() == 1);

        public virtual Cell RandomCell
        {
            get
            {
                var row = Random.Next(0, Rows);
                return this[row, Random.Next(0, rows[row].Count)];
            }
        }

        public PolarCell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows)
                {
                    return null;
                }

                if (column < 0)
                {
                    column = rows[row].Count - (Math.Abs(column) % rows[row].Count);
                }

                return rows[row][column % rows[row].Count];
            }
        }

        public IEnumerable<Cell> EachCell()
        {
            for (var row = 0; row < Rows; row++)
            {
                foreach (var cell in IterateRow(row))
                {
                    yield return cell;
                }
            }
        }

        public IEnumerable<IEnumerable<Cell>> EachRow()
        {
            for (var row = 0; row < rows.Length; row++)
            {
                yield return IterateRow(row);
            }
        }

        protected virtual void ConfigureNeighbors()
        {
            foreach (var cell in EachCell().Cast<PolarCell>())
            {
                if (cell.Row > 0)
                {
                    cell.Clockwise = this[cell.Row, cell.Column + 1];
                    cell.CounterClockwise = this[cell.Row, cell.Column - 1];
                    var ratio = rows[cell.Row].Count / rows[cell.Row - 1].Count;

                    var parent = rows[cell.Row - 1][cell.Column / ratio];
                    parent.Outward.Add(cell);
                    cell.Inward = parent;
                }
            }
        }

        protected virtual List<PolarCell>[] Prepare(Func<int, int, PolarCell> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var rowHeight = 1.0 / Rows;
            rows = new List<PolarCell>[Rows];
            rows[0] = new List<PolarCell> { new PolarCell(0, 0) };

            for (var row = 1; row < rows.Length; row++)
            {
                rows[row] = new List<PolarCell>();
                var radius = row / (float)Rows;
                var circumference = 2 * Math.PI * radius;

                var previous = rows[row - 1].Count;
                var estimatedCellWidth = circumference / previous;
                var ratio = (int)(estimatedCellWidth / rowHeight);
                var cells = previous * ratio;

                rows[row] = new List<PolarCell>(cells);
                for (var col = 0; col < cells; col++)
                {
                    rows[row].Add(create.Invoke(row, col));
                }
            }

            return rows;
        }

        private IEnumerable<PolarCell> IterateRow(int row)
        {
            for (var col = 0; col < rows[row].Count; col++)
            {
                yield return this[row, col];
            }
        }

        private bool IsOutOfBounds(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                return true;
            }

            if (column < 0 || column >= rows[row].Count)
            {
                return true;
            }

            return false;
        }
    }
}
