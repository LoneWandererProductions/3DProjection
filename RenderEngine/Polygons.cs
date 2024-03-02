using System;
using System.Collections.Generic;
using Mathematics;
using SkiaSharp;

namespace RenderEngine
{
    /// <inheritdoc cref="Geometry" />
    /// <summary>
    ///     Our Polygon Object
    /// </summary>
    /// <seealso cref="Geometry" />
    /// <seealso cref="IDrawable" />
    public sealed class Polygons : Geometry, IDrawable
    {
        public List<Coordinate2D> Path { get; set; } = new();

        public void Draw(SKCanvas canvas, SKPaint paint, GraphicStyle style)
        {
            using var path = CreatePath();

            // Fill or stroke the polygon
            switch (style)
            {
                case GraphicStyle.Mesh:
                    canvas.DrawPath(path, paint);
                    break;
                case GraphicStyle.Fill:
                {
                    using var fillPaint = new SKPaint {Style = SKPaintStyle.Fill};
                    canvas.DrawPath(path, fillPaint);
                }
                    break;
                case GraphicStyle.Plot:
                    foreach (var plot in Path) canvas.DrawPoint(plot.X, plot.Y, paint);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private SKPath CreatePath()
        {
            var path = new SKPath();

            path.MoveTo(Start.X, Start.Y); // Move to the starting point

            foreach (var line in Path) path.LineTo(line.X, line.Y); // Line to the points

            path.Close(); // Close the path to complete the polygon

            return path;
        }
    }
}