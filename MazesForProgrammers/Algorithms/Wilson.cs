using System.Collections.Generic;
using System.Linq;
using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class Wilson : IBuildMaze
    {
        public IGrid<Cell> ApplyTo(IGrid<Cell> grid)
        {
            var unvisited = grid.EachCell().ToList();

            var first = unvisited.Sample();
            unvisited.Remove(first);

            while (unvisited.Any())
            {
                var cell = unvisited.Sample();
                var path = new List<Cell> { cell };

                while (unvisited.Contains(cell))
                {
                    cell = cell.Neighbors.Sample();
                    var position = path.IndexOf(cell);

                    if (position > -1)
                    {
                        path = path.Take(position + 1).ToList();
                    }
                    else
                    {
                        path.Add(cell);
                    }
                }

                for (var i = 0; i < path.Count - 1; i++)
                {
                    path[i].AddLink(path[i + 1]);
                    unvisited.Remove(path[i]);
                }
            }

            return grid;
        }
    }
}
