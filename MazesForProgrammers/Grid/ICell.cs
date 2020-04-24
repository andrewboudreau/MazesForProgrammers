using System;
using System.Collections.Generic;

namespace MazesForProgrammers.Grid
{
    public interface ICell<T>
    {
        int X { get; }

        int Y { get; }

        (int X, int Y) Location => (X, Y);

        T Data { get; }

        (int X, int Y, T Data) Item => (X, Y, Data);
        
        (ICell<T> Top, ICell<T> Right, ICell<T> Bottom, ICell<T> Left) Neighbors { get; }

        void AddLink(ICell<T> cell, bool bidirectional = true);

        void RemoveLink(ICell<T> cell, bool bidirectional = true);

        IEnumerable<ICell<T>> Links { get; }
    }
}
