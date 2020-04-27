﻿using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public interface IGrid<T>
    {
        int Columns { get; }

        int Rows { get; }

        int Size { get; }

        ICell<T> RandomCell { get; }

        IEnumerable<ICell<T>> EachCell();

        IEnumerable<(int Row, IEnumerable<ICell<T>> Cells)> EachRow();
    }
}