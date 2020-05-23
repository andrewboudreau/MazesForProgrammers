using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Algorithms.Interfaces
{
    public interface IModifyMaze
    {
        IGrid<Cell> ApplyTo(IGrid<Cell> grid);
    }
}
