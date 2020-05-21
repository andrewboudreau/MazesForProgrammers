namespace MazesForProgrammers.DataStructures.Hex
{
    public class TriangleGrid : AbstractGrid<TriangleCell>
    {
        public TriangleGrid(int size = 3)
            : this(size, size)
        {
        }

        public TriangleGrid(int rows, int columns)
            : base(rows, columns, (row, column) => new TriangleCell(row, column))
        {
        }

        protected override void ConfigureNeighbors()
        {
            foreach (var cell in EachCell())
            {
                var row = cell.Row;
                var column = cell.Column;

                cell.West = this[row, column - 1];
                cell.East = this[row, column + 1];

                if (cell.Upright)
                {
                    cell.South = this[row + 1, column];
                }
                else
                {
                    cell.North = this[row - 1, column];
                }
            }
        }
    }
}
