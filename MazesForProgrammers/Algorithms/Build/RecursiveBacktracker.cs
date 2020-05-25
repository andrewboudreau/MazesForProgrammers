using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class RecursiveBacktracker : IBuildMaze
    {
        public IGrid ApplyTo(IGrid grid)
        {
            var current = grid.RandomCell;
            var stack = new Stack<Cell>();

            stack.Push(current);

            while (stack.Any())
            {
                current = stack.Pop();

                var neighbors = current.Neighbors.Where(x => x.Links.IsEmpty());
                if (neighbors.Any())
                {
                    var neighbor = neighbors.Sample();
                    current.AddLink(neighbor);

                    stack.Push(current);
                    stack.Push(neighbor);
                }
            }

            return grid;
        }
    }
}
