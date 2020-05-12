using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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

        public (int Row, int Column) Random()
        {
            if (masked.Count >= Rows * Columns)
            {
                throw new InvalidOperationException("Cannot select random cell when no cells exist.");
            }

            int row, column;
            while (true)
            {
                row = Grid.Random.Next(0, Rows);
                column = Grid.Random.Next(0, Columns);

                if (this[row, column])
                {
                    return (row, column);
                }
            }
        }

        private int RowColumnToIndex(int row, int column)
        {
            return (row * Columns) + column;
        }

        public static Mask FromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var rows = lines.First().Length;
            var columns = lines.Length;
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
    }
}
