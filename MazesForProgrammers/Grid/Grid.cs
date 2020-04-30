using System;
using System.Collections.Generic;
using MazesForProgrammers.Grid.Configuration;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid
{
    /// <summary>
    /// Manages a collection of <see cref="ICell{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of data for each cell in the grid.</typeparam>
    public class Grid<T> : IGrid<T>
    {
        private static readonly Random Random = new Random();

        private readonly ICell<T>[,] map;

        public Grid(int dimension = 3)
            : this(dimension, dimension, (row, col) => new Cell<T>(row, col))
        {
        }

        public Grid(int rows, int columns, Func<int, int, ICell<T>> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            if (rows < 2)
            {
                throw new ArgumentOutOfRangeException("Grid must contain 2 or more rows.");
            }

            if (columns < 2)
            {
                throw new ArgumentOutOfRangeException("Grid must contain 2 or more columns.");
            }

            Rows = rows;
            Columns = columns;

            map = Prepare(create);
        }

        public int Rows { get; }

        public int Columns { get; }

        public int Size => Rows * Columns;

        public ICell<T> RandomCell => this[(Random.Next(0, Rows), Random.Next(0, Columns))];

        public ICell<T> this[(int Row, int Column) location]
        {
            get
            {
                GuardBounds(location.Row, location.Column);
                return map[location.Row, location.Column];
            }
        }

        public IEnumerable<ICell<T>> EachCell()
        {
            for (var row = 0; row < map.GetLength(0); row++)
            {
                foreach (var cell in IterateRow(row))
                {
                    yield return cell;
                }
            }
        }

        public IEnumerable<(int Row, IEnumerable<ICell<T>> Cells)> EachRow()
        {
            for (var row = 0; row < map.GetLength(0); row++)
            {
                yield return (row, IterateRow(row));
            }
        }

        public bool InBounds((int Row, int Column) location)
        {
            var row = location.Row;
            if (row < 0 || row >= Rows)
            {
                return false;
            }

            var column = location.Column;
            if (column < 0 || column >= Columns)
            {
                return false;
            }

            return true;
        }

        protected virtual ICell<T>[,] Prepare(Func<int, int, ICell<T>> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var map = new ICell<T>[Rows, Columns];

            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    map[row, col] = create(row, col);
                }
            }

            return map;
        }

        public void ConfigureNeighbors(IConfigureNeighbors configure)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            foreach (var cell in EachCell())
            {
                configure.ConfigureNeighbors(cell, this);
            }
        }
        public void ClearLinksAndNeighbors()
        {
            foreach (var cell in EachCell())
            {
                cell.Clear();
            }
        }

        private IEnumerable<ICell<T>> IterateRow(int row)
        {
            for (var col = 0; col < map.GetLength(1); col++)
            {
                yield return this[(row, col)];
            }
        }

        private void GuardBounds(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row), $"'{row}' is invalid row, value must be between 0 and {Rows - 1}");
            }

            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), $"'{column}' is invalid column, value must be between 0 and {Columns - 1}");
            }
        }
    }
}
