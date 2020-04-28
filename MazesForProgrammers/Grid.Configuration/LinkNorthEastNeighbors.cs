using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Grid.Configuration
{
    public class LinkNorthEastNeighbors : IConfigureNeighbors
    {
        public LinkNorthEastNeighbors(bool clear = false)
        {
            Clear = clear;
        }

        public bool Clear { get; }

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
