using SkiaSharp;

namespace RenderEngine
{
    /// <summary>
    /// </summary>
    /// <seealso cref="RenderEngine.Geometry" />
    /// <seealso cref="RenderEngine.IDrawable" />
    public sealed class Circle : Geometry, IDrawable
    {
        public int Radius { get; set; }

        public void Draw(SKCanvas canvas, SKPaint paint, GraphicStyle style)
        {
            switch (style)
            {
                case GraphicStyle.Mesh:
                    paint.Style = SKPaintStyle.Stroke;
                    break;
                case GraphicStyle.Fill:
                    paint.Style = SKPaintStyle.Fill;
                    break;
                case GraphicStyle.Plot:
                    canvas.DrawPoint(Start.X, Start.Y, paint);
                    break;
            }

            canvas.DrawCircle(Start.X, Start.Y, Radius, paint);
        }
    }
}