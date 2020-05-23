using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Hex;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public partial class ImageRender
    {
        public Image Render(IGrid<TriangleCell> grid, Distances distances, int pixelsPerCell)
        {
            void draw(Graphics gfx, PointF[] quad, Cell cell)
            {
                var brush = new Pen(distances.Color(cell), 1).Brush;

                gfx.FillPolygon(brush, quad);
                gfx.DrawPolygon(new Pen(Color.Black, 1), quad);
            }

            return Render(grid, draw, pixelsPerCell);
        }

        public Image Render(IGrid<TriangleCell> grid, Action<Graphics, PointF[], Cell> cellRenderer, int size)
        {
            void draw(Graphics gfx, PointF[] quad, Cell cell)
            {
                gfx.DrawPolygon(new Pen(Color.Black, 1), quad);
            }

            cellRenderer = cellRenderer ?? draw;

            // cell spacing
            var halfWidth = size / 2.0;
            var height = size * Math.Sqrt(3) / 2.0;
            var halfHeight = height / 2.0;

            // canvas size
            var imageWidth = (int)(size * (grid.Columns + 1) / 2.0);
            var imageHeight = (int)(height * grid.Rows);

            /// drawing
            var background = Color.White;
            var foreground = Color.Black;

            var image = new Bitmap(imageWidth + 1, imageHeight + 1);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 1);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();

            // image border
            // graphics.DrawRectangle(pen, 0, 0, imageWidth, imageHeight);
            foreach (var cell in grid.EachCell())
            {
                var cx = halfWidth + cell.Column * halfWidth;
                var cy = halfHeight + cell.Row * height;

                var west = (int)(cx - halfWidth);
                var middle = (int)(cx);
                var east = (int)(cx + halfWidth);

                int apex, @base;
                if (cell.Upright)
                {
                    apex = (int)(cy - halfHeight);
                    @base = (int)(cy + halfHeight);
                }
                else
                {
                    apex = (int)(cy + halfHeight);
                    @base = (int)(cy - halfHeight);
                }

                var poly = new List<PointF>()
                {
                    new PointF(west, @base),
                    new PointF(middle, apex),
                    new PointF(east, @base),
                };

                // external renders now now have all the data, and run before walls being drawn.
                cellRenderer.Invoke(graphics, poly.ToArray(), cell);
            }

            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }
    }
}
