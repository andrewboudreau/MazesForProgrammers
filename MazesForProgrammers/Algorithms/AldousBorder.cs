using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class AldousBorder : IBuildMaze
    {
        public Grid ApplyTo(Grid grid)
        {
            var cell = grid.RandomCell;
            var unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                var neighbor = cell.Neighbors.Sample();
                if (neighbor.Links.IsEmpty())
                {
                    cell.AddLink(neighbor);
                    unvisited -= 1;
                }

                cell = neighbor;
            }

            return grid;
        }
    }
}
