using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Configuration
{
    public class SetNorthEastNeighbors : IConfigureNeighbors
    {
        public void ConfigureNeighbors<T>(ICell<T> cell, IGrid<T> grid)
        {
            var north = cell.North();
            if (grid.InBounds(north))
            {
                cell.Neighbors.Add(grid[north]);
            }

            var east = cell.East();
            if (grid.InBounds(east))
            {
                cell.Neighbors.Add(grid[east]);
            }
        }
    }
}
