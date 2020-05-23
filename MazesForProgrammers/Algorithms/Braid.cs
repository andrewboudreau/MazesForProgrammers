using System;
using System.Linq;

using MazesForProgrammers.Algorithms.Interfaces;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Algorithms
{
    public class Braid : IModifyMaze
    {
        private readonly int percent;

        public Braid(int percent)
        {
            this.percent = percent;
        }
        public IGrid<Cell> ApplyTo(IGrid<Cell> grid)
        {
            // Shuffle all the deadends
            foreach (var cell in grid.DeadEnds.Shuffle().ToList())
            {
                // Ensure still a deadend and above the random threshold.
                if (cell.Links.Count() == 1 && RandomSource.Random.Next(100) > percent)
                {
                    var neighbhors = cell.Neighbors.Where(x => x.Linked(cell) == false);

                    if (neighbhors.Count() == 0)
                    {
                        continue;
                        // throw new ArgumentNullException(nameof(neighbhors));
                    }

                    // Prefer to connect to another deadend.
                    var best = neighbhors.FirstOrDefault(x => x.Links.Count() == 1);
                    cell.AddLink(best ?? neighbhors.Sample());
                }
            }

            return grid;
        }
    }
}
