using System.Windows;
using Mathematics;

namespace LightVector
{
    public static class Converter
    {
        public static Point ToPoint(Vector3D vector)
        {
            return new Point {X = vector.X, Y = vector.Y};
        }

        public static Point ToPointR(Vector3D vector)
        {
            return new Point { X = vector.RoundedX, Y = vector.RoundedY };
        }

        public static Point ToPoint(Vector2D vector)
        {
            return new Point { X = vector.X, Y = vector.Y };
        }

        public static Point ToPointR(Vector2D vector)
        {
            return new Point { X = vector.RoundedX, Y = vector.RoundedY };
        }

        public static Vector2D ToPVector2D(Point point)
        {
            return new Vector2D { X = point.X, Y = point.Y };
        }

        public static System.Drawing.Point ToPoint(Point point)
        {
            return new System.Drawing.Point { X = (int)point.X, Y = (int)point.Y };
        }

        public static Point ToPoint(System.Drawing.Point point)
        {
            return new Point { X = point.X, Y = point.Y };
        }
    }
}