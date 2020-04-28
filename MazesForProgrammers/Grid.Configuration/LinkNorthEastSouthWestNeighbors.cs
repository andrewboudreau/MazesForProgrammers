using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Configuration
{
    public class LinkNorthEastSouthWestNeighbors : IConfigureNeighbors
    {
        public bool Clear => true;

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

            var south = cell.South();
            if (grid.InBounds(south))
            {
                cell.Neighbors.Add(grid[south]);
            }

            var west = cell.West();
            if (grid.InBounds(west))
            {
                cell.Neighbors.Add(grid[west]);
            }
        }
    }
}
