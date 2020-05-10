using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using MazesForProgrammers.DataStructures;

namespace MazesForProgrammers.Render
{
    public class ImageRender
    {
        public Image Render(Grid grid, Distances distances)
        {
            void draw(Graphics gfx, Rectangle rect, Cell cell)
            {
                var brush = new Pen(distances.BackgroundColor(cell)).Brush;
                gfx.FillRectangle(brush, rect);
            }

            return Render(grid, draw, 100);
        }

        public Image Render(Grid grid, Action<Graphics, Rectangle, Cell> cellRenderer, int pixelsPerCell = 100)
        {
            var width = pixelsPerCell * grid.Columns;
            var height = pixelsPerCell * grid.Rows;

            var background = Color.LightBlue;
            var foreground = Color.Black;

            var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 5);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();

            foreach (var cell in grid.EachCell())
            {
                var x1 = cell.Column * pixelsPerCell;
                var y1 = cell.Row * pixelsPerCell;
                var x2 = ((cell.Column + 1) * pixelsPerCell) - 1;
                var y2 = ((cell.Row + 1) * pixelsPerCell) - 1;

                cellRenderer.Invoke(graphics, new Rectangle(x1, y1, pixelsPerCell, pixelsPerCell), cell);

                if (!cell.Linked(cell.East))
                {
                    graphics.DrawLine(pen, x2, y1, x2, y2);
                }

                if (!cell.Linked(cell.South))
                {
                    graphics.DrawLine(pen, x1, y2, x2, y2);
                }
            }

            // Draw the map border, simplifies the wall render logic.
            graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);
            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }
    }
}
