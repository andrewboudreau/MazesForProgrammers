using System;
using System.Drawing;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Render.Extensions
{
    public enum MazeColor
    {
        White = 0,
        Teal,
        Purple,
        Mustard,
        Rust,
        Green,
        Blue,
    }

    public static class DistancesExtensions
    {
        public static Color Color(this Distances distances, Cell cell)
        {
            return Color(distances, cell, MazeColor.Green);
        }

        public static Color Color(this Distances distances, Cell cell, MazeColor color)
        {
            var intensity = distances.Intensity(cell);
            var dark = (int)(255 * intensity);
            var bright = (int)(128 + (127 * intensity));

            switch (color)
            {
                case MazeColor.White:
                    return System.Drawing.Color.White;
                case MazeColor.Teal:
                    return System.Drawing.Color.FromArgb(dark, bright, bright);
                case MazeColor.Purple:
                    return System.Drawing.Color.FromArgb(bright, dark, bright);
                case MazeColor.Mustard:
                    return System.Drawing.Color.FromArgb(bright, bright, dark);
                case MazeColor.Rust:
                    return System.Drawing.Color.FromArgb(bright, dark, dark);
                case MazeColor.Green:
                    return System.Drawing.Color.FromArgb(dark, bright, dark);
                case MazeColor.Blue:
                    return System.Drawing.Color.FromArgb(dark, dark, bright);
                default:
                    return System.Drawing.Color.FromArgb(dark, bright, bright);
            }
        }

        public static MazeColor RandomMazeColor()
        {
            return RandomEnumValue<MazeColor>();
        }

        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(RandomSource.Random.Next(v.Length));
        }
    }
}
