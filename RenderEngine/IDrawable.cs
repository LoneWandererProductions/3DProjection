using Mathematics;
using SkiaSharp;

namespace RenderEngine
{
    public interface IDrawable
    {
        SKColor Color { get; }
        Coordinate2D Start { get; }
        void Draw(SKCanvas canvas, SKPaint paint, GraphicStyle style);
    }
}