namespace MazesForProgrammers.DataStructures.Hex
{
    public class HexGrid : AbstractGrid<HexCell>
    {
        public HexGrid(int rows, int columns)
            : base(rows, columns, (row, column) => new HexCell(row, column))
        {
        }

        protected override void ConfigureNeighbors()
        {
            foreach (var cell in EachCell())
            {
                var row = cell.Row;
                var column = cell.Column;
                int northDiagonal, southDiagonal;

                if (cell.Column % 2 == 0)
                {
                    northDiagonal = row - 1;
                    southDiagonal = row;
                }
                else
                {
                    northDiagonal = row;
                    southDiagonal = row + 1;
                }

                cell.NorthWest = this[northDiagonal, column - 1];
                cell.North = this[row - 1, column];
                cell.NorthEast = this[northDiagonal, column + 1];

                cell.SouthWest = this[southDiagonal, column - 1];
                cell.South = this[row + 1, column];
                cell.SouthEast = this[southDiagonal, column + 1];
            }
        }
    }
}