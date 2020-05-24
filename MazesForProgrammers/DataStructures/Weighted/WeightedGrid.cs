namespace MazesForProgrammers.DataStructures.Weighted
{
    public class WeightedGrid : AbstractGrid<WeightedCell>
    {
        public WeightedGrid(int rows)
            : this(rows, rows)
        {
        }

        public WeightedGrid(int rows, int columns)
            : base(rows, columns, (row, column) => new WeightedCell(row, column))
        {
        }

        protected override void ConfigureNeighbors()
        {
            foreach (var cell in EachCell())
            {
                if (cell is null)
                {
                    return;
                }

                cell.North = this[cell.Row - 1, cell.Column];
                cell.South = this[cell.Row + 1, cell.Column];
                cell.East = this[cell.Row, cell.Column + 1];
                cell.West = this[cell.Row, cell.Column - 1];
            }
        }
    }
}
