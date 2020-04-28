using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Configuration
{
    public interface IConfigureNeighbors
    {
        bool Clear { get; }

        void ConfigureNeighbors<T>(ICell<T> cell, IGrid<T> grid);
    }
}