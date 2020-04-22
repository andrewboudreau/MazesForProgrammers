using System;
using System.Collections.Generic;
using System.Text;

namespace MazesForProgrammers
{
    public class MazeBuilder : IMazeBuilder
    {
        public MazeBuilder()
        {
        }

        public string Build()
        {
            return "foo";
        }
    }

    public interface IMazeBuilder
    {
        string Build();
    }
}
