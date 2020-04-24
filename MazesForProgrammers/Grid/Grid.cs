using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace MazesForProgrammers.Grid
{
    public class Grid<T>
    {
        private static readonly Random Random = new Random();

        private readonly ICell<T>[,] map;

        public Grid(int dimension, Func<(int X, int Y), ICell<T>> create = null)
        {
            Dimension = dimension;
            // map = Prepare(createCell ?? ((x, y)) => new Cell(x, y));
            Configure();

        }

        public int Dimension { get; }

        public int Size => Dimension * Dimension;

        ICell<T> RandomCell => map[Random.Next(0, Dimension - 1), Random.Next(0, Dimension - 1)];

        public IEnumerator<(int X, int Y, T Data)> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected virtual ICell<T>[,] Prepare(Func<(int X, int Y), ICell<T>> create)
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
                    map[y, x] = create((x, y));
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
