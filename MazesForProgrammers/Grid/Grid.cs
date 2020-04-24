using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace MazesForProgrammers.Grid
{
    /// <summary>
    /// Manages a collection of <see cref="ICell{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of data for each cell in the grid.</typeparam>
    public class Grid<T>
    {
        private static readonly Random Random = new Random();

        private readonly ICell<T>[,] map;

        public Grid(int dimension)
            : this(dimension, dimension, Cell<T>.DefaultCreate<T>)
        {
        }

        public Grid(int dimension, Func<int, int, ICell<T>> create)
            : this(dimension, dimension, create)
        {
        }

        public Grid(int rows, int columns, Func<int, int, ICell<T>> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }
            create ??= Cell<T>.DefaultCreate<T>;
            map = Prepare(create);
            Configure();
        }

        public int Rows { get; }

        public int Columns { get; }

        public int Size => Rows * Columns;

        ICell<T> RandomCell => map[Random.Next(0, Rows - 1), Random.Next(0, Columns - 1)];

        public IEnumerator<(int X, int Y, T Data)> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected virtual ICell<T>[,] Prepare(Func<int, int, ICell<T>> create)
        {
            if (create is null)
            {
                throw new ArgumentNullException(nameof(create));
            }

            var map = new ICell<T>[Dimension, Dimension];

            for (var y = 0; y < Dimension; y++)
            {
                for (var x = 0; x < Dimension; x++)
                {
                    map[y, x] = create(x, y);
                }
            }

            return map;
        }

        protected virtual void Configure()
        {
            foreach (var cell in map)
            {
                Console.WriteLine($"Cell X={cell.X} Y={cell.Y}");
            }
        }
    }
}
