using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Algorithms
{
    public interface ICreateMazes
    {
        ICreateMazes SetupNeighbors<T>(IGrid<T> grid);

        IGrid<T> ApplyTo<T>(IGrid<T> grid);
    }
}
