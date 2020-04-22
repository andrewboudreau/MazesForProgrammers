using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public class Grid<T>
    {
        private readonly ICell<T>[,] map;

        public Grid(int dimension)
        {
            int i = 0, x, y;
            map = new ICell<T>[dimension, dimension];

            foreach (var cell in map)
            {
                y = i / dimension;
                x = i % dimension;
                map[y, x] = new DefaultCell<T>(x, y);
                i++;
            }
        }

        public IEnumerator<(int X, int Y, T Data)> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DefaultCell<T> : ICell<T>
    {
        public DefaultCell(int x, int y)
        {
            X = x;
            Y = y;
            Data = default;
        }

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public T Data { get; }
    }

    public class FourWalls
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
    }
}
