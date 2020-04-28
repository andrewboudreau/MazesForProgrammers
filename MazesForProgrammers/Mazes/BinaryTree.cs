using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid;
using MazesForProgrammers.Grid.Configuration;
using System.Linq;

namespace MazesForProgrammers.Mazes
{
    public class BinaryTree
    {
        public void ApplyTo<T>(IGrid<T> grid)
        {
            grid.ConfigureNeighbors(new LinkNorthEastNeighbors());

            foreach (var cell in grid.EachCell())
            {
                if (cell.Neighbors.Any())
                {
                    cell.AddLink(cell.Neighbors.Sample());
                }
            }
        }
    }
}
