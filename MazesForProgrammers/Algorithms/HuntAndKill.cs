using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class HuntAndKill : IBuildMaze
    {
        public Grid ApplyTo(Grid grid)
        {
            var current = grid.RandomCell;

            while (current != null)
            {
                // Kill unvisited neighbors
                var unvisitedNeighbors = current.Neighbors.Where(x => x.Links.IsEmpty());
                if (unvisitedNeighbors.Any())
                {
                    var neighbor = unvisitedNeighbors.Sample();
                    current.AddLink(neighbor);
                    current = neighbor;
                }
                else
                {
                    // Hunt for a unvisited cell with a linked neighbor.
                    current = null;

                    foreach (var cell in grid.EachCell())
                    {
                        if (cell.Links.IsEmpty() && cell.Neighbors.Any(x => x.Links.Any()))
                        {
                            current = cell;
                            var neighbor = cell.Neighbors.Where(x => x.Links.Any()).Sample();
                            current.AddLink(neighbor);
                            break;
                        }
                    }
                }
            }

            return grid;
        }
    }
}
