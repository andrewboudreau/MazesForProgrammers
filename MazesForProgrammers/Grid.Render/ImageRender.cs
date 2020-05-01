using System.Drawing;
using System.Drawing.Drawing2D;
using MazesForProgrammers.Extensions;
using MazesForProgrammers.Grid.Interfaces;

namespace MazesForProgrammers.Grid.Render
{
    public class ImageRender
    {
        public Image Render<T>(IGrid<T> grid, int pixelsPerCell = 100)
        {
            var width = pixelsPerCell * grid.Columns;
            var height = pixelsPerCell * grid.Rows;

            var background = Color.LightBlue;
            var foreground = Color.Black;

            var image = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground);
            graphics.Clear(background);

            // Tranforms coordinates system from 
            //    - Position (0,0) being upper left to position (0, 0) being bottom left in coordinate system.
            //    - Positive y-axis is now from bottom (0) to top (height) of the image.
            GraphicsContainer containerState = graphics.BeginContainer();
            graphics.TranslateTransform(0, height - 1);
            graphics.ScaleTransform(1.0f, -1.0f);

            // Draw the map border, simplifies the wall render logic.
            graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);
            foreach (var cell in grid.EachCell())
            {
                var x1 = cell.Column * pixelsPerCell;
                var y1 = cell.Row * pixelsPerCell;
                var x2 = ((cell.Column + 1) * pixelsPerCell) - 1;
                var y2 = ((cell.Row + 1) * pixelsPerCell) - 1;

                if (grid.InBounds(cell.East()))
                {
                    if (!cell.Linked(grid[cell.East()]))
                    {
                        graphics.DrawLine(pen, x2, y1, x2, y2);
                    }
                }

                if (grid.InBounds(cell.North()))
                {
                    if (!cell.Linked(grid[cell.North()]))
                    {
                        graphics.DrawLine(pen, x1, y2, x2, y2);
                    }
                }
            }

            graphics.EndContainer(containerState);
            graphics.Save();
            return image;
        }
    }
}
