using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Hex;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public partial class ImageRender
    {
        public Image Render(IGrid<HexCell> grid, Distances distances, int pixelsPerCell)
        {
            void draw(Graphics gfx, PointF[] quad, Cell cell)
            {
                var brush = new Pen(distances.Color(cell), 1).Brush;
                gfx.FillPolygon(brush, quad);
            }

            return Render(grid, draw, pixelsPerCell);
        }

        public Image Render(IGrid<HexCell> grid, Action<Graphics, PointF[], Cell> cellRenderer, int pixelsPerCell)
        {
            // cell spacing
            var aSize = pixelsPerCell / 2.0;
            var bSize = pixelsPerCell * Math.Sqrt(3) / 2.0;
            var width = pixelsPerCell * 2;
            var height = bSize * 2;

            // canvas size
            var imageWidth = (int)(3 * aSize * grid.Columns + aSize + 0.5);
            var imageHeight = (int)(height * grid.Rows + bSize + 0.5);

            /// drawing
            var background = Color.White;
            var foreground = Color.Black;

            var image = new Bitmap(imageWidth + 1, imageHeight + 1);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 1);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();
            //graphics.DrawRectangle(pen, 0, 0, imageWidth, imageHeight);
            foreach (var cell in grid.EachCell())
            {
                var cx = pixelsPerCell + 3 * cell.Column * aSize;
                var cy = bSize + cell.Row * height;
                if (cell.Column % 2 != 0)
                {
                    // odd column
                    cy += bSize;
                }

                // f,n = far, near
                // n, s, e, w = north, south, east, west
                var farWest = (int)(cx - pixelsPerCell);
                var nearWest = (int)(cx - aSize);
                var nearEast = (int)(cx + aSize);
                var farEast = (int)(cx + pixelsPerCell);

                var north = (int)(cy - bSize);
                var middle = (int)cy;
                var south = (int)(cy + bSize);

            }

            foreach (var cell in grid.EachCell())
            {
                var cx = pixelsPerCell + 3 * cell.Column * aSize;
                var cy = bSize + cell.Row * height;
                if (cell.Column % 2 != 0)
                {
                    // odd column
                    cy += bSize;
                }

                var farWest = (int)(cx - pixelsPerCell);
                var nearWest = (int)(cx - aSize);
                var nearEast = (int)(cx + aSize);
                var farEast = (int)(cx + pixelsPerCell);

                var north = (int)(cy - bSize);
                var middle = (int)cy;
                var south = (int)(cy + bSize);

                var poly = new PointF[]
                {
                    new PointF(farWest, middle),
                    new PointF(nearWest, north),
                    new PointF(nearEast, north),
                    new PointF(farEast, middle),
                    new PointF(nearEast, south),
                    new PointF(nearWest, south)
                };

                // external renders now now have all the data, and run before walls being drawn.
                cellRenderer.Invoke(graphics, poly, cell);

                // draw cell walls
                if (cell.SouthWest is null || !cell.Linked(cell.SouthWest))
                {
                    graphics.DrawLine(pen, farWest, middle, nearWest, south);
                }

                if (cell.NorthWest is null || !cell.Linked(cell.NorthWest))
                {
                    graphics.DrawLine(pen, farWest, middle, nearWest, north);
                }

                if (cell.North is null || !cell.Linked(cell.North))
                {
                    graphics.DrawLine(pen, nearWest, north, nearEast, north);
                }

                if (!cell.Linked(cell.NorthEast))
                {
                    graphics.DrawLine(pen, nearEast, north, farEast, middle);
                }

                if (!cell.Linked(cell.SouthEast))
                {
                    graphics.DrawLine(pen, farEast, middle, nearEast, south);
                }

                if (!cell.Linked(cell.South))
                {
                    graphics.DrawLine(pen, nearEast, south, nearWest, south);
                }

                //// Console.WriteLine($"ax:{ax} ay:{ay}, bx:{bx} by:{by}, cx:{cx} cy:{cy}, dx:{dx} dy:{dy}");
            }

            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }
    }
}
