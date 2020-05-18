using System;
using System.Collections.Generic;
using System.Linq;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Algorithms
{
    public interface IBuildMaze
    {
        IGrid<Cell> ApplyTo(IGrid<Cell> grid);
    }

    public interface IBuildRectangleMaze
    {
        RectangleGrid ApplyTo(RectangleGrid grid);
    }

    public static class IBuildMazeExtensions
    {
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
