using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public partial class ImageRender
    {
        private static readonly Action<Graphics, Rectangle, Cell> nodraw = (x, y, z) => { };

        public Image Render(IGrid<RectangleCell> grid, int pixelsPerCell)
        {
            return Render(grid, nodraw, pixelsPerCell);
        }

        public Image Render(IGrid<RectangleCell> grid, Distances distances, int pixelsPerCell)
        {
            void draw(Graphics gfx, Rectangle rect, Cell cell)
            {
                var brush = new Pen(distances.Color(cell)).Brush;
                gfx.FillRectangle(brush, rect);
            }

            return RenderInset(grid, draw, pixelsPerCell, 30);
        }

        public Image Render(IGrid<RectangleCell> grid, Action<Graphics, Rectangle, RectangleCell> cellRenderer, int pixelsPerCell)
        {
            var width = pixelsPerCell * grid.Columns;
            var height = pixelsPerCell * grid.Rows;

            var background = Color.White;
            var foreground = Color.Black;

            var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 2);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();

            foreach (var cell in grid.EachCell())
            {
                var x1 = cell.Column * pixelsPerCell;
                var y1 = cell.Row * pixelsPerCell;
                var x2 = ((cell.Column + 1) * pixelsPerCell);
                var y2 = ((cell.Row + 1) * pixelsPerCell);

                cellRenderer.Invoke(graphics, new Rectangle(x1, y1, pixelsPerCell, pixelsPerCell), cell);

                if (!cell.Linked(cell.East))
                {
                    graphics.DrawLine(pen, x2, y1, x2, y2);
                }

                if (!cell.Linked(cell.South))
                {
                    graphics.DrawLine(pen, x1, y2, x2, y2);
                }

                if (!cell.Linked(cell.North))
                {
                    graphics.DrawLine(pen, x1, y1, x2, y1);
                }

                if (!cell.Linked(cell.West))
                {
                    graphics.DrawLine(pen, x1, y1, x1, y2);
                }
            }

            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }

        public Image RenderInset(IGrid<RectangleCell> grid, Action<Graphics, Rectangle, RectangleCell> cellRenderer, int pixelsPerCell, int insetPercent)
        {
            var width = pixelsPerCell * grid.Columns;
            var height = pixelsPerCell * grid.Rows;
            var inset = (int)((insetPercent / 150.0) * pixelsPerCell);

            var background = Color.White;
            var foreground = Color.Black;

            var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 2);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();

            foreach (var cell in grid.EachCell())
            {
                if (cell is null)
                {
                    continue;
                }

                var x = cell.Column * pixelsPerCell;
                var y = cell.Row * pixelsPerCell;

                var x1 = x;
                var x4 = x + pixelsPerCell;
                var x2 = x1 + inset;
                var x3 = x4 - inset;

                var y1 = y;
                var y4 = y + pixelsPerCell;
                var y2 = y1 + inset;
                var y3 = y4 - inset;

                cellRenderer.Invoke(graphics, new Rectangle(x1, y1, pixelsPerCell, pixelsPerCell), cell);

                if (cell.Linked(cell.North))
                {
                    graphics.DrawLine(pen, x2, y1, x2, y2);
                    graphics.DrawLine(pen, x3, y1, x3, y2);
                }
                else
                {
                    graphics.DrawLine(pen, x2, y2, x3, y2);
                }

                if (cell.Linked(cell.South))
                {
                    graphics.DrawLine(pen, x2, y3, x2, y4);
                    graphics.DrawLine(pen, x3, y3, x3, y4);
                }
                else
                {
                    graphics.DrawLine(pen, x2, y3, x3, y3);
                }


                if (cell.Linked(cell.West))
                {
                    graphics.DrawLine(pen, x1, y2, x2, y2);
                    graphics.DrawLine(pen, x1, y3, x2, y3);
                }
                else
                {
                    graphics.DrawLine(pen, x2, y2, x2, y3);
                }

                if (cell.Linked(cell.East))
                {
                    graphics.DrawLine(pen, x3, y2, x4, y2);
                    graphics.DrawLine(pen, x3, y3, x4, y3);
                }
                else
                {
                    graphics.DrawLine(pen, x3, y2, x3, y3);
                }
            }

            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }

    }
}
