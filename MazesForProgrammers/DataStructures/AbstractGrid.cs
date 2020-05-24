using System;
using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// Basic functionality for working with a collection of <see cref="Cell"/>.
    /// </summary>
    public abstract partial class AbstractGrid<T> : IGrid<T> where T : Cell
    {
        protected IEnumerable<IEnumerable<T>> rows;

        public AbstractGrid(int rows, int columns, Func<int, int, T> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            if (rows < 2)
            {
                throw new ArgumentOutOfRangeException("Grid must contain 2 or more rows.");
            }

            Rows = rows;
            Columns = columns;

            this.rows = Prepare(create);
            ConfigureNeighbors();
        }

        /// <inheritdoc />
        public int Rows { get; }

        /// <inheritdoc />
        public int Columns { get; }

        /// <inheritdoc />
        public virtual int Size => Rows * Columns;

        /// <inheritdoc />
        public virtual T this[int row, int column]
        {
            get
            {
                if (IsOutOfBounds(row, column))
                {
                    return null;
                }

                return rows.ElementAt(row).ElementAt(column);
            }
        }

        /// <inheritdoc />
        public virtual T RandomCell
        {
            get
            {
                var row = RandomSource.Random.Next(0, Rows);
                var column = RandomSource.Random.Next(0, rows.ElementAt(row).Count());
                return this[row, column];
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> DeadEnds => EachCell().RemoveNulls().Where(x => x.Links.Count() == 1);

        /// <inheritdoc />
        public virtual IEnumerable<T> EachCell()
        {
            for (var row = 0; row < rows.Count(); row++)
            {
                foreach (var cell in IterateRow(row))
                {
                    yield return cell;
                }
            }
        }

        /// <inheritdoc />
        public virtual IEnumerable<IEnumerable<T>> EachRow()
        {
            for (var row = 0; row < rows.Count(); row++)
            {
                yield return IterateRow(row);
            }
        }

        /// <summary>
        /// Configures all <see cref="Cell"/> neighbors.
        /// </summary>
        protected abstract void ConfigureNeighbors();

        /// <summary>
        /// Provides a way to enumerate each item in a row.
        /// </summary>
        /// <param name="row">The row of <see cref="Cell"/> to enumerate.</param>
        /// <returns>All of the <see cref="Cell"/> in a row.</returns>
        protected virtual IEnumerable<T> IterateRow(int row)
        {
            for (var col = 0; col < rows.ElementAt(row).Count(); col++)
            {
                yield return this[row, col];
            }
        }

        /// <summary>
        /// Sets the results of <paramref name="create"/> for every <see cref="Cell"/>.
        /// </summary>
        /// <param name="create">A <see cref="Cell"/> factory which creates a cell for a give row and column location.</param>
        /// <returns>A grid with all of the cells instantiated, but not linked.</returns>
        protected virtual IEnumerable<IEnumerable<T>> Prepare(Func<int, int, T> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var rows = new List<List<T>>(Rows);

            for (var row = 0; row < Rows; row++)
            {
                var column = new List<T>(Columns);
                for (var col = 0; col < Columns; col++)
                {
                    column.Add(create(row, col));
                }

                rows.Add(column);
            }

            return rows;
        }

        private bool IsOutOfBounds(int row, int column)
        {
            if (row < 0 || row >= rows.Count())
            {
                return true;
            }

            if (column < 0 || column >= rows.ElementAt(row).Count())
            {
                return true;
            }

            return false;
        }
    }
}