using System;
using System.Collections.Generic;
using System.Linq;
using MazesForProgrammers.Configuration;
using MazesForProgrammers.Extensions;

namespace MazesForProgrammers.DataStructures
{
    /// <summary>
    /// Manages a collection of <see cref="Cell"/>.
    /// </summary>
    public class Grid
    {
        public static Random Random = new Random(0);

        public static void SetRandom(int? seed = null)
        {
            if (seed.HasValue)
            {
                Random = new Random(seed.Value);
            }
            else
            {
                Random = new Random();
            }
        }

        private readonly Cell[,] map;

        public Grid(int dimension = 3)
            : this(dimension, dimension, (row, col) => new Cell(row, col))
        {
        }

        public Grid(int rows, int columns, Func<int, int, Cell> create)
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
            ConfigureNeighbors(new SetNorthEastSouthWestNeighbors());
        }

        public int Rows;
        public int Columns;

        public virtual int Size => Rows * Columns;

        public virtual Cell RandomCell => this[Random.Next(0, Rows), Random.Next(0, Columns)];

        public IEnumerable<Cell> DeadEnds => EachCell().RemoveNulls().Where(x => x.Links.Count() == 1);

        public Cell this[int row, int column]
        {
            get
            {
                if (IsOutOfBounds(row, column))
                {
                    return null;
                }

                return map[row, column];
            }
        }

        public IEnumerable<Cell> EachCell()
        {
            for (var row = 0; row < map.GetLength(0); row++)
            {
                foreach (var cell in IterateRow(row))
                {
                    yield return cell;
                }
            }
        }

        public IEnumerable<IEnumerable<Cell>> EachRow()
        {
            for (var row = 0; row < map.GetLength(0); row++)
            {
                yield return IterateRow(row);
            }
        }

        protected void ConfigureNeighbors(IConfigureNeighbors configure)
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

        protected virtual Cell[,] Prepare(Func<int, int, Cell> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var map = new Cell[Rows, Columns];

            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    map[row, col] = create(row, col);
                }
            }

            return map;
        }

        private IEnumerable<Cell> IterateRow(int row)
        {
            for (var col = 0; col < map.GetLength(1); col++)
            {
                yield return this[row, col];
            }
        }

        private bool IsOutOfBounds(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                return true;
            }

            if (column < 0 || column >= Columns)
            {
                return true;
            }

            return false;
        }
    }
}
