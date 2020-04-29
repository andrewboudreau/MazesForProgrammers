using System.Linq;
using System.Text;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Render
{
    public class CellDebugger
    {
        public string Render<T>(IGrid<T> grid)
        {
            var output = new StringBuilder();
            foreach (var row in grid.EachRow().Reverse())
            {
                output.AppendLine($"GridRow: {row.Row}");
                foreach (var cell in row.Cells)
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
