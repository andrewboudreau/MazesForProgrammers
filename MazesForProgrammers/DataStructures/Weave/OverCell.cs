using System.Collections.Generic;

namespace MazesForProgrammers.DataStructures.Weave
{
    public class OverCell : RectangleCell, IWeaveCell
    {
        private readonly ICanTunnelUnder tunneler;

        public OverCell(int row, int column, ICanTunnelUnder tunneler)
            : base(row, column)
        {
            this.tunneler = tunneler;
        }

        new public RectangleCell North => base.North as RectangleCell;
        new public RectangleCell South => base.South as RectangleCell;
        new public RectangleCell East => base.East as RectangleCell;
        new public RectangleCell West => base.West as RectangleCell;

        public override IEnumerable<Cell> Neighbors
        {
            get
            {
                foreach (var cell in base.Neighbors)
                {
                    yield return cell;
                }

                if (CanTunnelNorth)
                {
                    yield return North.North;
                }

                if (CanTunnelSouth)
                {
                    yield return South.South;
                }

                if (CanTunnelEast)
                {
                    yield return East.East;
                }

                if (CanTunnelWest)
                {
                    yield return West.West;
                }
            }
        }

        public override void AddLink(Cell cell, bool bidirectional = true)
        {
            RectangleCell neighbor = null;
            if (cell is OverCell rectangleCell)
            {
                if (North is OverCell && North == rectangleCell.South)
                {
                    neighbor = North;
                }

                else if (South is OverCell && South == rectangleCell.North)
                {
                    neighbor = South;
                }

                if (East is OverCell && East == rectangleCell.West)
                {
                    neighbor = East;
                }

                if (West is OverCell && West == rectangleCell.East)
                {
                    neighbor = West;
                }
            }

            if (neighbor is OverCell)
            {
                tunneler.TunnelUnder(neighbor as OverCell);
            }
            else
            {
                base.AddLink(cell, bidirectional);
            }
        }

        public bool IsHorizontalPassage => Linked(East) && Linked(West) && Linked(North) && !Linked(South);

        public bool IsVerticalPassage => Linked(North) && Linked(South) && !Linked(East) && !Linked(West);


        protected bool CanTunnelNorth => North?.North != null && ((North as OverCell)?.IsHorizontalPassage ?? false);

        protected bool CanTunnelSouth => South?.South != null && ((South as OverCell)?.IsHorizontalPassage ?? false);

        protected bool CanTunnelEast => East?.East != null && ((East as OverCell)?.IsVerticalPassage ?? false);

        protected bool CanTunnelWest => West?.West != null && ((West as OverCell)?.IsVerticalPassage ?? false);
    }
}
