using Mathematics;
using SkiaSharp;

namespace RenderEngine
{
    public class CubicBezierCurve : Geometry, IDrawable
    {
        /// <summary>
        ///     Gets or sets the first control point.
        /// </summary>
        /// <value>
        ///     The first.
        /// </value>
        public Coordinate2D First { get; set; }

        /// <summary>
        ///     Gets or sets the second control point.
        /// </summary>
        /// <value>
        ///     The first.
        /// </value>
        public Coordinate2D Second { get; set; }

        /// <summary>
        ///     Gets or sets the third control point or Ending point.
        /// </summary>
        /// <value>
        ///     The first.
        /// </value>
        public Coordinate2D Third { get; set; }

        public int StrokeWidth { get; set; } = 3;

        public void Draw(SKCanvas canvas, SKPaint paint, GraphicStyle style)
        {
            using var path = new SKPath();
            // Move to the starting point
            path.MoveTo(Start.X, Start.Y);

            // Draw a cubic Bezier curve
            path.CubicTo(First.X, First.Y, Second.X, Second.Y, Third.X, Third.Y);

            switch (style)
            {
                // Choose drawing style
                case GraphicStyle.Mesh:
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = StrokeWidth;
                    break;
                case GraphicStyle.Fill:
                {
                    using var fillPaint = new SKPaint {Style = SKPaintStyle.Fill};
                    canvas.DrawPath(path, fillPaint);
                    return; // No need to draw the stroke in the Fill style
                }
                case GraphicStyle.Plot:
                    paint.StrokeWidth = StrokeWidth;
                    canvas.DrawPoint(First.X, First.Y, paint);
                    canvas.DrawPoint(Second.X, Second.Y, paint);
                    canvas.DrawPoint(Third.X, Third.Y, paint);
                    break;
            }

            // Draw the path
            canvas.DrawPath(path, paint);
        }
    }
}