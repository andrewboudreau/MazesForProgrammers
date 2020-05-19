using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class BinarySearch : IBuildMaze
    {
        public IGrid<Cell> ApplyTo(IGrid<Cell> grid)
        {
            foreach (var cell in grid.EachCell())
            {
                var north = grid[cell.Row, cell.Column - 1];
                var east = grid[cell.Row + 1, cell.Column];

                var options = new Cell[] { north, east }.RemoveNulls();
                if (options.Any())
                {
                    cell.AddLink(options.Sample());
                }
            }

            return grid;
        }
    }
}
