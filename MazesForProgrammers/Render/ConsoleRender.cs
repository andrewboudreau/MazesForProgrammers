using System;
using System.Linq;
using System.Text;
using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Render
{
    public class ConsoleRender
    {
        private readonly Func<Cell, string> cellRender;

        public ConsoleRender(bool debug = false)
            : this(cell => debug ? $"{cell.Row},{cell.Column}" : "   ")
        {
        }

        public ConsoleRender(Func<Cell, string> cellRender)
        {
            this.cellRender = cellRender;
        }

        public string Render(IGrid<RectangleCell> grid)
        {
            if (grid.Rows > 20 || grid.Columns > 20)
            {
                Console.WriteLine($"Grid with {grid.Rows} rows and {grid.Columns} columns is too big for Console.Write.");
            }

            var output = "+" + string.Concat(Enumerable.Repeat("---+", grid.Columns)) + Environment.NewLine;

            foreach (var row in grid.EachRow())
            {
                var top = "|";
                var bottom = "+";

                foreach (var cell in row)
                {
                    var body = cellRender.Invoke(cell);
                    var east = "|";

                    if (cell?.Linked(cell.East) ?? false)
                    {
                        east = " ";
                    }

                    top += body + east;

                    var south = string.Concat(Enumerable.Repeat("-", 3));
                    if (cell?.Linked(cell.South) ?? false)
                    {
                        south = string.Concat(Enumerable.Repeat(" ", 3));
                    }

                    var corner = "+";
                    bottom += south + corner;
                }

                output += top + Environment.NewLine;
                output += bottom + Environment.NewLine;
            }

            return output;
        }
        
        public string Debug(RectangleGrid grid)
        {
            var output = new StringBuilder();
            foreach (var row in grid.EachRow().Reverse())
            {
                foreach (var cell in row)
                {
                    output.AppendLine($"{cell} -- ");
                    output.AppendLine("\tNeighbors:" + string.Join(',', cell.Neighbors));
                    output.AppendLine("\tLinks:" + string.Join(',', cell.Links));
                }

                output.AppendLine();
            }

            return output.ToString();
        }
    }
}
