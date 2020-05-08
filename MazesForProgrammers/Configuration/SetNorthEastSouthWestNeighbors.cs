using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Configuration
{
    public class SetNorthEastSouthWestNeighbors : IConfigureNeighbors
    {
        public void ConfigureNeighbors(Cell cell, Grid grid)
        {
            cell.North = grid[cell.Row - 1, cell.Column];
            cell.South = grid[cell.Row + 1, cell.Column];

            cell.East = grid[cell.Row, cell.Column + 1];
            cell.West = grid[cell.Row, cell.Column - 1];
        }
    }
}
