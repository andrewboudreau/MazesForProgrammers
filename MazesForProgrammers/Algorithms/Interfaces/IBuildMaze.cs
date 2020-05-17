using MazesForProgrammers.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.Algorithms
{
    public interface IBuildMaze
    {
        IGrid ApplyTo(IGrid grid);
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
