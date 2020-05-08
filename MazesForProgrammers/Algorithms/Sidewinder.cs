using System.Collections.Generic;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class SideWinder : IBuildMaze
    {
        public Grid ApplyTo(Grid grid)
        {
            var run = new List<Cell>(10);

            foreach (var cells in grid.EachRow())
            {
                run.Clear();
                foreach (var cell in cells)
                {
                    run.Add(cell);
                    var isNorthernBoundary = cell.Row == grid.Rows - 1;
                    var isEasternBoundary = cell.Column == grid.Columns - 1;

                    var rand = Grid.Random.Next(2) == 0;
                    var shouldCloseRun = isEasternBoundary || (!isNorthernBoundary && rand);
                    //// Console.WriteLine($"Cell:{cell} - {(isNorthernBoundary ? "North, " : "")} {(isEasternBoundary ? "East, " : "")} {(shouldCloseRun ? "Close, " : "")} Random:{rand}");

                    if (shouldCloseRun)
                    {
                        var passage = run.Sample();
                        if (passage.North is Cell)
                        {  //// Console.WriteLine($"\t Adding North Passage Link to {passage.North}");
                            passage.AddLink(passage.North);
                            run.Clear();
                        
                        }
                    }
                    else
                    {
                        //// Console.WriteLine($"\t Extending east to {cell.East}");
                        cell.AddLink(cell.East);
                    }

                    //// Console.WriteLine();
                }
            }

            return grid;
        }
    }
}
