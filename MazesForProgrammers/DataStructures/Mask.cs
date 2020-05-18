using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MazesForProgrammers.DataStructures
{
    public class Mask
    {
        private readonly HashSet<int> masked = new HashSet<int>();

        public Mask(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public int Rows { get; }

        public int Columns { get; }

        public bool this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                {
                    return false;
                }

                var index = RowColumnToIndex(row, column);
                return !masked.Contains(index);
            }

            set
            {
                var index = RowColumnToIndex(row, column);
                if (value)
                {
                    masked.Remove(index);
                }
                else
                {
                    masked.Add(index);
                }
            }
        }

        public int Count()
        {
            return (Rows * Columns) - masked.Count;
        }

        public int[] Random()
        {
            if (masked.Count >= Rows * Columns)
            {
                throw new InvalidOperationException("More than the max # of cells have been masked.");
            }

            int row, column;
            while (true)
            {
                row = RandomSource.Random.Next(0, Rows);
                column = RandomSource.Random.Next(0, Columns);

                if (this[row, column])
                {
                    return new int[2] { row, column };
                }
            }
        }

        private int RowColumnToIndex(int row, int column)
        {
            return (row * Columns) + column;
        }

        public static Mask FromTextFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var columns = lines.First().Length;
            var rows = lines.Length;
            var mask = new Mask(rows, columns);

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (lines[row][column] == 'X')
                    {
                        mask[row, column] = false;
                    }
                }
            }

            return mask;
        }

        public static Mask FromImage(string file)
        {
            using (var bitmap = new Bitmap(file))
            {
                return FromImage(bitmap);
            }
        }

        public static Mask FromImage(Bitmap bitmap)
        {
            var mask = new Mask(bitmap.Height, bitmap.Width);
            for (var row = 0; row < mask.Rows; row++)
            {
                for (var column = 0; column < mask.Columns; column++)
                {
                    var color = bitmap.GetPixel(column, row);
                    var isWhite = color.R == 255 && color.G == 255 && color.B == 255;
                    mask[row, column] = isWhite;
                }
            }

            return mask;
        }
    }
}
