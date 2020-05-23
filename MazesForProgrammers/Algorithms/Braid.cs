using System;
using System.Linq;

using MazesForProgrammers.Algorithms.Interfaces;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Algorithms
{
    /// <summary>
    /// Braids a grid by extending deadends. This algorithm favors linking to other deadends.
    /// </summary>
    public class Braid : IModifyMaze
    {
        private readonly int percent;

        /// <summary>
        /// Create a new braid instance.
        /// </summary>
        /// <param name="percent">Value between 0 and 100, the percentage of the total deadends extend.</param>
        public Braid(int percent)
        {
            this.percent = Math.Clamp(percent, 0, 100);
        }

        /// <summary>
        /// Introduces a percentage of the deadends as loops in the grid.
        /// </summary>
        /// <param name="grid">The grid to braid.</param>
        /// <returns>The braided grid.</returns>
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
