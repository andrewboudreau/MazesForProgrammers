using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Configuration
{
    public interface IConfigureNeighbors
    {
        void ConfigureNeighbors(Cell cell, Grid grid);
    }
}