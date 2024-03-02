using System.Collections.Generic;
using System.Windows.Controls;
using Mathematics;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace RenderEngine
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    ///     Our Render Control
    /// </summary>
    public partial class RenderView
    {
        private SKBitmap _bitmap;

        public RenderView()
        {
            InitializeComponent();
        }

        public bool Debug { get; set; }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (_bitmap == null) _bitmap = new SKBitmap(e.Info.Width, e.Info.Height);

            e.Surface.Canvas.DrawBitmap(_bitmap, 0, 0);

            skiaElement.InvalidateVisual();
        }

        public void Clear()
        {
            if (_bitmap == null) return;

            using var canvas = new SKCanvas(_bitmap);
            canvas.Clear(SKColors.White);

            skiaElement.InvalidateVisual();
        }

        public void Draw(IEnumerable<object> objects, GraphicStyle style, bool clear)
        {
            Clear();

            foreach (var item in objects)
                switch (item)
                {
                    case Polygons polygons:
                        DrawShape(polygons, style, clear);
                        break;
                    case Rectangular rectangular:
                        DrawShape(rectangular, style, clear);
                        break;
                    case CubicBezierCurve curve:
                        DrawShape(curve, style, clear);
                        break;
                    case Circle circle:
                        DrawShape(circle, style, clear);
                        break;
                }
        }

        private void DrawShape<T>(T shape, GraphicStyle style, bool clear) where T : IDrawable
        {
            using var paint = new SKPaint();
            using var canvas = new SKCanvas(_bitmap);

            if (clear) canvas.Clear(SKColors.White);

            paint.Color = shape.Color;

            switch (style)
            {
                case GraphicStyle.Mesh:
                    paint.Style = SKPaintStyle.Stroke;
                    break;
                case GraphicStyle.Fill:
                    paint.Style = SKPaintStyle.Fill;
                    break;
                case GraphicStyle.Plot:
                    DrawPlot(canvas, shape.Start);
                    shape.Draw(canvas, paint, style);
                    break;
            }

            if (style != GraphicStyle.Plot) shape.Draw(canvas, paint, style);

            skiaElement.InvalidateVisual();
        }

        public SKColor GetPixel(int x, int y)
        {
            return (_bitmap != null && x >= 0 && x < _bitmap.Width && y >= 0 && y < _bitmap.Height)
                ? _bitmap.GetPixel(x, y)
                : SKColors.Transparent;
        }

        public void SetPixel(int x, int y, SKColor color)
        {
            if (_bitmap != null && x >= 0 && x < _bitmap.Width && y >= 0 && y < _bitmap.Height)
            {
                _bitmap.SetPixel(x, y, color);
                skiaElement.InvalidateVisual();
            }
        }

        private static void DrawPlot(SKCanvas canvas, Coordinate2D plot)
        {
            canvas.DrawPoint(plot.X, plot.Y, new SKPaint());
        }
    }
}