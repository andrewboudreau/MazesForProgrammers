using System;

namespace MazesForProgrammers.DataStructures
{
    public class MaskedRectangleGrid : RectangleGrid
    {
        private readonly Mask mask;

        public MaskedRectangleGrid(Mask mask)
            : base(mask.Rows, mask.Columns, MaskedCellFactory(mask))
        {
            this.mask = mask;
        }

        public override RectangleCell RandomCell
        {
            get
            {
                var rowColumn = mask.Random();
                return this[rowColumn[0], rowColumn[1]];
            }
        }

        public override int Size => mask.Count();

        public static Func<int, int, RectangleCell> MaskedCellFactory(Mask mask)
        {
            return (row, column) =>
            {
                return mask[row, column] ? new RectangleCell(row, column) : null;
            };
        }
    }
}