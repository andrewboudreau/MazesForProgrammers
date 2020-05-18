using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class AldousBroder : IBuildMaze
    {
        public IGrid<Cell> ApplyTo(IGrid<Cell> grid)
        {
            var current = grid.RandomCell;
            var unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                var neighbor = current.Neighbors.Sample();
                if (neighbor.Links.IsEmpty())
                {
                    current.AddLink(neighbor);
                    unvisited -= 1;
                }

                current = neighbor;
            }

            return grid;
        }
    }
}
