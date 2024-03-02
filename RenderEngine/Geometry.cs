using Mathematics;
using SkiaSharp;

namespace RenderEngine
{
    public class Geometry
    {
        public Geometry()
        {
        }

        public Geometry(Coordinate2D start, SKColor color)
        {
            Start = start;
            Color = color;
        }

        public Coordinate2D Start { get; set; }

        public SKColor Color { get; set; } = SKColors.Blue;
    }
}