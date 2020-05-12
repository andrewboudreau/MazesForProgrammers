using System;

namespace MazesForProgrammers.DataStructures
{
    public class MaskedGrid : Grid
    {
        private readonly Mask mask;

        public MaskedGrid(Mask mask)
            : base(mask.Rows, mask.Columns, MaskedCellFactory(mask))
        {
            this.mask = mask;
        }

        public override Cell RandomCell
        {
            get
            {
                var (Row, Column) = mask.Random();
                return this[Row, Column];
            }
        }

        public override int Size => mask.Count();

        public static Func<int, int, Cell> MaskedCellFactory(Mask mask)
        {
            return (row, column) =>
            {
                return mask[row, column] ? new Cell(row, column) : null;
            };
        }
    }
}