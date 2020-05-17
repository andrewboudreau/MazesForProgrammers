using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures
{
    public interface IGrid
    {
        int Rows { get; }

        int Columns { get; }

        int Size { get; }

        Cell RandomCell { get; }

        IEnumerable<IEnumerable<Cell>> EachRow();

        IEnumerable<Cell> EachCell();
    }
}