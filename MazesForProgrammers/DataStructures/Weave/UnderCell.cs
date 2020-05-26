namespace MazesForProgrammers.DataStructures.Weave
{
    public class UnderCell : RectangleCell, IWeaveCell
    {
        public UnderCell(OverCell cell)
            : base(cell.Row, cell.Column)
        {
            if (cell.IsHorizontalPassage)
            {
                North = cell.North;
                South = cell.South;

                cell.North.South = this;
                cell.South.North = this;

                AddLink(North);
                AddLink(South);
            }
            else
            {
                East = cell.East;
                West = cell.West;

                cell.East.West = this;
                cell.West.West = this;
                AddLink(East);
                AddLink(West);
            }
        }

        bool IWeaveCell.IsHorizontalPassage => East is Cell || West is Cell;

        bool IWeaveCell.IsVerticalPassage => North is Cell || South is Cell;
    }
}
