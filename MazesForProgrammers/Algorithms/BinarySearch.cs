using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class BinarySearch : IBuildRectangleMaze
    {
        public RectangleGrid ApplyTo(RectangleGrid grid)
        {
            foreach (var cell in grid.EachCell())
            {
                var options = new Cell[] { cell.North, cell.East }.RemoveNulls();
                if (options.Any())
                {
                    cell.AddLink(options.Sample());
                }
            }

            return grid;
        }
    }
}
