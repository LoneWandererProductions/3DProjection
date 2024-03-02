using System;
using System.Collections.Generic;
using Mathematics;
using SkiaSharp;

namespace RenderEngine
{
    /// <summary>
    ///     Basic Rectangular Object
    /// </summary>
    /// <seealso cref="Geometry" />
    public sealed class Rectangular : Geometry, IDrawable
    {
        public int Height { get; set; }

        public int Width { get; set; }

        public void Draw(SKCanvas canvas, SKPaint paint, GraphicStyle style)
        {
            var c1 = new Coordinate2D(Start.X + Width, Start.Y);
            var c2 = new Coordinate2D(Start.X, Start.Y - Height);
            var c3 = new Coordinate2D(Start.X + Width, Start.Y - Height);
            var path = new List<Coordinate2D> {c1, c2, c3};
            var skPath = CreatePath(path);

            // Fill or stroke the Rectangle
            switch (style)
            {
                case GraphicStyle.Mesh:
                    canvas.DrawPath(skPath, paint);
                    break;
                case GraphicStyle.Fill:
                {
                    using var fillPaint = new SKPaint {Style = SKPaintStyle.Fill};
                    canvas.DrawPath(skPath, fillPaint);
                }
                    break;
                case GraphicStyle.Plot:
                    foreach (var plot in path) canvas.DrawPoint(plot.X, plot.Y, paint);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private SKPath CreatePath(IEnumerable<Coordinate2D> path)
        {
            var skPath = new SKPath();

            skPath.MoveTo(Start.X, Start.Y); // Move to the starting point

            foreach (var line in path) skPath.LineTo(line.X, line.Y); // Line to the points

            skPath.Close(); // Close the path to complete the polygon

            return skPath;
        }
    }
}