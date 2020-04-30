﻿using System;
using System.Collections.Generic;

using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Extensions
{
    public static class ICellExtensions
    {
        public static (int Row, int Column) North<T>(this ICell<T> cell)
        {
            return (cell.Row + 1, cell.Column);
        }

        public static (int Row, int Column) East<T>(this ICell<T> cell)
        {
            return (cell.Row, cell.Column + 1);
        }

        public static (int Row, int Column) South<T>(this ICell<T> cell)
        {
            return (cell.Row - 1, cell.Column);
        }

        public static (int Row, int Column) West<T>(this ICell<T> cell)
        {
            return (cell.Row, cell.Column - 1);
        }

        public static (int Row, int Column) NorthEast<T>(this ICell<T> cell)
        {
            return (cell.Row + 1, cell.Column + 1);
        }

        public static (int Row, int Column) SouthEast<T>(this ICell<T> cell)
        {
            return (cell.Row + 1, cell.Column - 1);
        }

        public static (int Row, int Column) SouthWest<T>(this ICell<T> cell)
        {
            return (cell.Row - 1, cell.Column - 1);
        }

        public static (int Row, int Column) NorthWest<T>(this ICell<T> cell)
        {
            return (cell.Row - 1, cell.Column + 1);
        }

        public static IEnumerable<ICell<T>> Perform<T>(this IEnumerable<ICell<T>> cells, Action<ICell<T>> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var cell in cells)
            {
                action.Invoke(cell);
            }

            return cells;
        }
    }
}
