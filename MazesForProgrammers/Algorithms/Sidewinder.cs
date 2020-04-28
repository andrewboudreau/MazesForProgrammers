using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Configuration;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Mazes
{
    public class SideWinder : ICreateMazes
    {
        public ICreateMazes SetupNeighbors<T>(IGrid<T> grid)
        {
            grid.ConfigureNeighbors(new LinkNorthEastNeighbors());
            return this;
        }

        public IGrid<T> ApplyTo<T>(IGrid<T> grid)
        {
            foreach (var cell in grid.EachCell())
            {
                if (cell.Neighbors.Any())
                {
                    cell.AddLink(cell.Neighbors.Sample());
                }
            }

            return grid;
        }
    }
}
