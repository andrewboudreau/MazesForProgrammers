using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace MazesForProgrammers.Grid
{
    public class Cell<T> : ICell<T>
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public T Data => throw new NotImplementedException();

        public (ICell<T> Top, ICell<T> Right, ICell<T> Bottom, ICell<T> Left) Neighbors => throw new NotImplementedException();

        public IEnumerable<ICell<T>> Links => throw new NotImplementedException();

        public void AddLink(ICell<T> cell, bool bidirectional = true)
        {
            throw new NotImplementedException();
        }

        public void RemoveLink(ICell<T> cell, bool bidirectional = true)
        {
            throw new NotImplementedException();
        }

        protected virtual void Prepare()
        {

        }

        protected virtual void Configure()
        {
        }
    }
}
