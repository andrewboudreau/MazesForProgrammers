using System;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.DataStructures.Polar
{
    public class PolarGrid : AbstractGrid<PolarCell>
    {
        public PolarGrid(int rows = 6)
            : this(rows, (row, col) => new PolarCell(row, col))
        {
        }

        public PolarGrid(int rows, Func<int, int, PolarCell> create)
            : base(rows, 0, create)
        {
        }

        public new int Columns
        {
            get
            {
                throw new NotImplementedException($"The number of columns varies for {nameof(PolarGrid)} and is not a single value.");
            }
        }

        public override int Size => EachRow().Sum(x => x.Count());

        public override PolarCell this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows)
                {
                    return null;
                }

                if (column < 0)
                {
                    column = rows.ElementAt(row).Count() - (Math.Abs(column) % rows.ElementAt(row).Count());
                }

                return rows.ElementAt(row).ElementAt(column % rows.ElementAt(row).Count());
            }
        }

        protected override void ConfigureNeighbors()
        {
            foreach (var cell in EachCell())
            {
                if (cell.Row > 0)
                {
                    cell.Clockwise = this[cell.Row, cell.Column + 1];
                    cell.CounterClockwise = this[cell.Row, cell.Column - 1];
                    var ratio = rows.ElementAt(cell.Row).Count() / rows.ElementAt(cell.Row - 1).Count();

                    var parent = rows.ElementAt(cell.Row - 1).ElementAt(cell.Column / ratio);
                    parent.Outward.Add(cell);
                    cell.Inward = parent;
                }
            }
        }

        protected override IEnumerable<IEnumerable<PolarCell>> Prepare(Func<int, int, PolarCell> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var rowHeight = 1.0 / Rows;
            var map = new List<PolarCell>[Rows];

            map[0] = new List<PolarCell> { new PolarCell(0, 0) };

            for (var row = 1; row < Rows; row++)
            {
                map[row] = new List<PolarCell>();
                var radius = row / (float)Rows;
                var circumference = 2 * Math.PI * radius;

                var previous = map[row - 1].Count;
                var estimatedCellWidth = circumference / previous;
                var ratio = (int)(estimatedCellWidth / rowHeight);
                var cells = previous * ratio;

                map[row] = new List<PolarCell>(cells);
                for (var col = 0; col < cells; col++)
                {
                    map[row].Add(create.Invoke(row, col));
                }
            }

            return map;
        }
    }
}
