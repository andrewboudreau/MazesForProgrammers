using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using MazesForProgrammers.DataStructures;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public partial class ImageRender
    {
        public Image Render(IGrid<WeightedCell> grid, int pixelsPerCell)
        {
            return Render(grid, (a, b, c) => { }, pixelsPerCell);
        }

        public Image Render(IGrid<WeightedCell> grid, Distances distances, int pixelsPerCell)
        {
            Font drawFont = new Font("Arial", 10);
            using var pen = new Pen(Color.Black, 2);

            void draw(Graphics gfx, Rectangle rect, WeightedCell cell)
            {
                var brush = new Pen(distances.Color(cell)).Brush;
                if (grid[cell.Row, cell.Column].Weight > 1)
                {
                    brush = new Pen(Color.IndianRed).Brush;
                }

                gfx.FillRectangle(brush, rect);
                if (distances[cell].GetValueOrDefault() != 0)
                {
                    gfx.DrawString(distances[cell].GetValueOrDefault().ToString(), drawFont, pen.Brush, rect);
                }
            }

            return Render(grid, draw, pixelsPerCell);
        }

        public Image Render(IGrid<WeightedCell> grid, Action<Graphics, Rectangle, WeightedCell> cellRenderer, int pixelsPerCell)
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
    }
}
