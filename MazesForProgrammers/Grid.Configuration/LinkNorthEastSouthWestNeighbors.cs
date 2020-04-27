using System;
using System.Collections.Generic;

using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Grid.Configuration
{
    public class LinkNorthEastSouthWestNeighbors : IConfigureNeighbors
    {
        public static Action<ICell<T>, Grid<T>> Configure<T>()
        {
            return (cell, grid) =>
            {
                var neighbors = new List<ICell<T>>();

                var north = cell.North();
                if (grid.InBounds(north))
                {
                    neighbors.Add(grid[north]);
                }

                var east = cell.East();
                if (grid.InBounds(east))
                {
                    neighbors.Add(grid[east]);
                }

                var south = cell.South();
                if (grid.InBounds(south))
                {
                    neighbors.Add(grid[south]);
                }

                var west = cell.West();
                if (grid.InBounds(west))
                {
                    neighbors.Add(grid[west]);
                }
            };
        }
    }
}
