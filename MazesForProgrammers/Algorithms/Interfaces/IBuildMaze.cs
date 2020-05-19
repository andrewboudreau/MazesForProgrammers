using System;
using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Algorithms
{
    /// <summary>
    /// The interface needed for an algorithm to build a maze on a grid.
    /// </summary>
    public interface IBuildMaze
    {
        /// <summary>
        /// Builds a maze by linking cells and forming passage ways.
        /// </summary>
        /// <param name="grid">A The grid to recieve the maze building algorithm.</param>
        /// <returns>The grid with a maze defined.</returns>
        IGrid<Cell> ApplyTo(IGrid<Cell> grid);
    }

    public static class IBuildMazeExtensions
    {
        /// <summary>
        /// Gets an instance of each known <see cref="IBuildMaze"/> algorithm.
        /// </summary>
        /// <returns>A collection of instances of maze building algorithms.</returns>
        public static IEnumerable<IBuildMaze> MazeBuilders()
        {
            var builderType = typeof(IBuildMaze);
            foreach (var type in builderType.Assembly.GetTypes().Where(p => builderType.IsAssignableFrom(p) && p.IsClass))
            {
                yield return (IBuildMaze)Activator.CreateInstance(type);
            }
        }
    }
}
