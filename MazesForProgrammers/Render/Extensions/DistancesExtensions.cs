using System.Drawing;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Render.Extensions
{
    public static class DistancesExtensions
    {
        public static Color Color(this Distances distances, Cell cell)
        {
            var intensity = distances.Intensity(cell);
            var dark = (int)(255 * intensity);
            var bright = (int)(128 + (127 * intensity));

            return System.Drawing.Color.FromArgb(255, dark, bright, dark);
        }
    }
}
