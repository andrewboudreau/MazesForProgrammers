using System;
using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures.Weave
{
    public class WeaveGrid : AbstractGrid<OverCell>, ICanTunnelUnder
    {
        private readonly List<UnderCell> underCells;

        public WeaveGrid(int dimension = 3)
            : this(dimension, dimension)
        {
        }

        public WeaveGrid(int rows, int columns)
            : base(rows, columns, (rows, columns) => null)
        {
            underCells = new List<UnderCell>();
        }

        protected override IEnumerable<IEnumerable<OverCell>> Prepare(Func<int, int, OverCell> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var rows = new List<List<OverCell>>(Rows);

            for (var row = 0; row < Rows; row++)
            {
                var column = new List<OverCell>(Columns);
                for (var col = 0; col < Columns; col++)
                {
                    column.Add(new OverCell(row, Columns, this));
                }

                rows.Add(column);
            }

            return rows;
        }

        public override IEnumerable<OverCell> EachCell()
        {
            foreach (var cell in base.EachCell())
            {
                yield return cell;
            }

            foreach(var cell in underCells)
            {
                //yield return cell;
            }
        }

        public void TunnelUnder(OverCell cell)
        {
            var underCell = new UnderCell(cell);
            underCells.Add(underCell);
        }

        protected override void ConfigureNeighbors()
        {
        }
    }
}
