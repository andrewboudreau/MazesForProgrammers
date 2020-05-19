﻿using System;
using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.Algorithms;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Mazes
{
    public class SideWinder : ISolveMaze
    {
        public IGrid<Cell> ApplyTo(IGrid<Cell> grid)
        {
            var run = new List<Cell>();
            foreach (var cells in grid.EachRow())
            {
                run.Clear();
                foreach (var cell in cells)
                {
                    run.Add(cell);
                    var isNorthernBoundary = grid[cell.Row, cell.Column - 1] is null;
                    var isEasternBoundary = grid[cell.Row + 1, cell.Column] is null;

                    var rand = RandomSource.Random.Next(2) == 0;
                    var shouldCloseRun = isEasternBoundary || (!isNorthernBoundary && rand);
                    //// System.Console.WriteLine($"Cell:{cell} - {(isNorthernBoundary ? "North Boundary, " : "")} {(isEasternBoundary ? "East Boundary, " : "")} {(shouldCloseRun ? "Close Run" : "")}");

                    if (shouldCloseRun)
                    {
                        var passage = run.Sample();
                        var north = grid[passage.Row, passage.Column - 1];
                        if (north is Cell)
                        {
                            //// System.Console.WriteLine($"\t Adding North Passage Link to {north}");
                            passage.AddLink(north);
                            run.Clear();
                        }
                    }
                    else
                    {
                        var east = grid[cell.Row + 1, cell.Column];
                        cell.AddLink(east);
                        //// System.Console.WriteLine($"\t Extending east to {east}");
                    }
                }
            }

            return grid;
        }
    }
}
