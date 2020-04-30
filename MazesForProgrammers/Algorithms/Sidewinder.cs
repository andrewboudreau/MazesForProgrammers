using System;
using System.Collections.Generic;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Configuration;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Mazes
{
    public class SideWinder : ICreateMazes
    {
        private static readonly Random Random = new Random();

        public ICreateMazes SetupNeighbors<T>(IGrid<T> grid)
        {
            grid.ClearLinksAndNeighbors();
            grid.ConfigureNeighbors(new SetNorthEastNeighbors());
            return this;
        }

        public IGrid<T> ApplyTo<T>(IGrid<T> grid)
        {
            var run = new List<ICell<T>>(10);

            foreach (var (Row, Cells) in grid.EachRow())
            {
                run.Clear();
                foreach (var cell in Cells)
                {
                    run.Add(cell);
                    var isNorthernBoundary = cell.Row == grid.Rows - 1;
                    var isEasternBoundary = cell.Column == grid.Columns - 1;

                    var rand = Random.Next(2) == 0;
                    var shouldCloseRun = isEasternBoundary || (!isNorthernBoundary && rand);
                    //// Console.WriteLine($"Cell:{cell} - {(isNorthernBoundary ? "North, " : "")} {(isEasternBoundary ? "East, " : "")} {(shouldCloseRun ? "Close, " : "")} Random:{rand}");

                    if (shouldCloseRun)
                    {
                        var passage = run.Sample();
                        if (grid.InBounds(passage.North()))
                        {
                            //// Console.WriteLine($"\t Adding North Passage Link to {grid[passage.North()]}");
                            passage.AddLink(grid[passage.North()]);
                            run.Clear();
                        }
                    }
                    else
                    {
                        //// Console.WriteLine($"\t Extending east to {grid[cell.East()]}");
                        cell.AddLink(grid[cell.East()]);
                    }

                    //// Console.WriteLine();
                }
            }

            return grid;
        }
    }
}
