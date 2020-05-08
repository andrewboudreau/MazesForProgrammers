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
            var run = new List<Cell>();
            foreach (var cells in grid.EachRow())
            {
                run.Clear();
                foreach (var cell in cells)
                {
                    run.Add(cell);
                    var isNorthernBoundary = cell.North is null;
                    var isEasternBoundary = cell.East is null;

                    var rand = Grid.Random.Next(2) == 0;
                    var shouldCloseRun = isEasternBoundary || (!isNorthernBoundary && rand);
                    //// System.Console.WriteLine($"Cell:{cell} - {(isNorthernBoundary ? "North Boundary, " : "")} {(isEasternBoundary ? "East Boundary, " : "")} {(shouldCloseRun ? "Close Run" : "")}");

                    if (shouldCloseRun)
                    {
                        var passage = run.Sample();
                        if (passage.North is Cell)
                        {
                            //// System.Console.WriteLine($"\t Adding North Passage Link to {passage.North}");
                            passage.AddLink(passage.North);
                            run.Clear();
                        }
                    }
                    else
                    {
                        //// System.Console.WriteLine($"\t Extending east to {cell.East}");
                        cell.AddLink(cell.East);
                    }
                }
            }

            return grid;
        }
    }
}
