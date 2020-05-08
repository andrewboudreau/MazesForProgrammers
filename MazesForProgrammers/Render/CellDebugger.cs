using System.Linq;
using System.Text;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Render
{
    public class CellDebugger
    {
        public string Render(Grid grid)
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
