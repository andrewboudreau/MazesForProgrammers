using System.Linq;

using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Render
{
    public class ConsoleRender
    {
        public string Render<T>(IGrid<T> grid, bool debug = false)
        {
            var output = "+" + string.Concat(Enumerable.Repeat("---+", grid.Columns)) + "\r\n";
            foreach (var row in grid.EachRow().Reverse())
            {
                var top = "|";
                var bottom = "+";

                foreach (var cell in row.Cells)
                {
                    var body = debug ? $"{row.Row},{cell.Column}" : "   ";
                    var east = "|";

                    if (grid.InBounds(cell.East()) && cell.Linked(grid[cell.East()]))
                    {
                        east = " ";
                    }

                    top += body + east;

                    var south = string.Concat(Enumerable.Repeat("-", 3));
                    if (grid.InBounds(cell.South()) && cell.Linked(grid[cell.South()]))
                    {
                        south = string.Concat(Enumerable.Repeat(" ", 3));
                    }

                    var corner = "+";
                    bottom += south + corner;
                }

                output += top + "\r\n";
                output += bottom + "\r\n";
            }

            return output;
        }
    }
}
