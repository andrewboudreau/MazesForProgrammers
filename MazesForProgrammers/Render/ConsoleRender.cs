using System.Linq;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.Render
{
    public class ConsoleRender
    {
        private const bool Debug = false;

        public string Render(Grid grid)
        {
            var output = "+" + string.Concat(Enumerable.Repeat("---+", grid.Columns)) + "\r\n";

            foreach (var row in grid.EachRow())
            {
                var top = "|";
                var bottom = "+";

                foreach (var cell in row)
                {
                    var body = Debug ? $"{cell.Row},{cell.Column}" : "   ";
                    var east = "|";

                    if (cell.Linked(cell.East))
                    {
                        east = " ";
                    }

                    top += body + east;

                    var south = string.Concat(Enumerable.Repeat("-", 3));
                    if (cell.Linked(cell.South))
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
