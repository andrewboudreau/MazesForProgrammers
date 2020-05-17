using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

using MazesForProgrammers.DataStructures;
using MazesForProgrammers.DataStructures.Polar;
using MazesForProgrammers.Render.Extensions;

namespace MazesForProgrammers.Render
{
    public partial class ImageRender
    {
        private static readonly Action<Graphics, PointF[], Cell> nodrawP = (x, y, z) => { };

        public Image Render(PolarGrid grid, int pixelsPerCell)
        {
            return Render(grid, nodrawP, pixelsPerCell);
        }

        public Image Render(PolarGrid grid, Distances distances, int pixelsPerCell)
        {
            void draw(Graphics gfx, PointF[] quad, Cell cell)
            {
                var brush = new Pen(distances.Color(cell)).Brush;
                gfx.FillPolygon(brush, quad);
            }

            return Render(grid, draw, pixelsPerCell);
        }

        public Image Render(PolarGrid grid, Action<Graphics, PointF[], Cell> cellRenderer, int pixelsPerCell)
        {
            var imageSize = 2 * grid.Rows * pixelsPerCell;
            var center = imageSize / 2;

            var background = Color.White;
            var foreground = Color.Black;

            var image = new Bitmap(imageSize + 1, imageSize + 1);
            using var graphics = Graphics.FromImage(image);
            using var pen = new Pen(foreground, 2);
            graphics.Clear(background);

            GraphicsContainer containerState = graphics.BeginContainer();
            graphics.DrawArc(pen, 0, 0, imageSize, imageSize, 0, 360);

            foreach (var baseCell in grid.EachCell().Skip(1))
            {
                var cell = baseCell as PolarCell;
                var length = grid.EachRow().Skip(cell.Row).First().Count();
                var theta = 2 * Math.PI / length;

                var inner = cell.Row * pixelsPerCell;
                var outer = (cell.Row + 1) * pixelsPerCell;
                var thetaCcw = cell.Column * theta;
                var thetaCw = (cell.Column + 1) * theta;

                var ax = (int)(center + (inner * Math.Cos(thetaCcw)));
                var ay = (int)(center + (inner * Math.Sin(thetaCcw)));

                var bx = (int)(center + (outer * Math.Cos(thetaCcw)));
                var by = (int)(center + (outer * Math.Sin(thetaCcw)));

                var cx = (int)(center + (inner * Math.Cos(thetaCw)));
                var cy = (int)(center + (inner * Math.Sin(thetaCw)));

                var dx = (int)(center + (outer * Math.Cos(thetaCw)));
                var dy = (int)(center + (outer * Math.Sin(thetaCw)));

                var poly = new List<PointF>()
                {
                    new PointF(ax, ay),
                    new PointF(bx, by)
                };

                var subdivisions = cell.Outward.Count;
                for (var subdivision = 1; subdivision <= subdivisions; subdivision++)
                {
                    var difference = Math.Abs(thetaCw - thetaCcw) / cell.Outward.Count;
                    var angle = thetaCcw + (difference * subdivision);
                    var x = (int)(center + (outer * Math.Cos(angle)));
                    var y = (int)(center + (outer * Math.Sin(angle)));
                    poly.Add(new PointF(x, y));
                }

                poly.Add(new PointF(dx, dy));
                poly.Add(new PointF(cx, cy));
                poly.Add(new PointF(ax, ay));

                cellRenderer.Invoke(graphics, poly.ToArray(), cell);

                if (!cell.Linked(cell.Inward))
                {
                    graphics.DrawLine(pen, ax, ay, cx, cy);
                }

                if (!cell.Linked(cell.Clockwise))
                {
                    graphics.DrawLine(pen, cx, cy, dx, dy);
                }

                if (cell.Outward.Count > 1)
                {
                    //Console.WriteLine($"THERE ARE MORE THAN 1 NEIGHBOR: ax:{ax} ay:{ay}, bx:{bx} by:{by}, cx:{cx} cy:{cy}, dx:{dx} dy:{dy}");
                }

                if (!cell.Linked(cell.CounterClockwise))
                {
                    graphics.DrawLine(pen, ax, ay, bx, by);
                }

                //// Console.WriteLine($"ax:{ax} ay:{ay}, bx:{bx} by:{by}, cx:{cx} cy:{cy}, dx:{dx} dy:{dy}");
            }

            graphics.EndContainer(containerState);
            graphics.Save();

            return image;
        }
    }
}
