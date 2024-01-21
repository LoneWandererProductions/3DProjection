/*
 * COPYRIGHT:   See COPYING in the top level directory
 * PROJECT:     AvalonsDen
 * FILE:        Wvg/Vectoricing.cs
 * PURPOSE:
 * PROGRAMER:   Peter Geinitz (Wayfarer)
 */

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LightVector;
using Mathematics;

namespace _3DProjection
{
    /// <summary>
    ///     https://stackoverflow.com/questions/15917611/drawing-line-to-next-point-in-realtime
    ///     https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/hit-testing-in-the-visual-layer#overriding_default_hit_testing
    /// </summary>
    public sealed partial class MainWindow
    {
        /// <summary>
        ///     The test height (const). Value: 300.
        /// </summary>
        private const int TestHeight = 300;

        /// <summary>
        ///     The test width (const). Value: 300.
        /// </summary>
        private const int TestWidth = 300;

        /// <summary>
        ///     The Vectoricing Interface.
        /// </summary>
        private static Vectors _vctr;

        /// <summary>
        ///     The points.
        /// </summary>
        private readonly Dictionary<int, Point> _points;

        /// <summary>
        ///     The count.
        /// </summary>
        private int _count;

        /// <summary>
        ///     The get Click.
        /// </summary>
        private bool _getClick;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ImageTest.Width = TestWidth;
            ImageTest.Height = TestHeight;
            _vctr = new Vectors();
            _points = new Dictionary<int, Point>();
        }

        /// <summary>
        ///     The image test mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The mouse button event arguments.</param>
        private void ImageTest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_getClick) return;

            var point = e.GetPosition(this);

            //TODO Hack for cursor!
            point.Y -= 25;
            _count++;

            _points.Add(_count, point);

            //Generate Point
            //Rewrite
            //GenerateLine();
        }

        /// <summary>
        ///     The Button resize click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnResize_Click(object sender, RoutedEventArgs e)
        {
            _vctr.Scale(2);
            Redraw();
        }

        /// <summary>
        ///     The generate line.
        /// </summary>
        private void GenerateLine()
        {
            if (_points.Count <= 1) return;

            var pointone = _points.Last().Value;
            var pointTwo = _points[_count - 1];

            var line = new LineObject
            {
                StartPoint = new Point(pointone.X, pointone.Y),
                EndPoint = new Point(pointTwo.X, pointTwo.Y)
            };

            _vctr.LineAdd(line);

            AddImage(line);
        }

        /// <summary>
        ///     The redraw.
        /// </summary>
        private void Redraw()
        {
            ImageTest.Children.Clear();

            if (_vctr.Lines == null) return;

            foreach (var line in _vctr.Lines) AddImage(line);
        }

        /// <summary>
        ///     Add the image.
        /// </summary>
        /// <param name="line">The line.</param>
        private void AddImage(LineObject line)
        {
            var newLine = new Line
            {
                X1 = line.StartPoint.X,
                Y1 = line.StartPoint.Y,
                X2 = line.EndPoint.X,
                Y2 = line.EndPoint.Y,
            };

            ImageTest.Children.Add(newLine);
        }

        /// <summary>
        ///     The Button rotate click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnRotate_Click(object sender, RoutedEventArgs e)
        {
            _vctr.Rotate(90);
            //ImageTest.Width = _testWidth * 2;
            //ImageTest.Height = _testHeight * 2;
            Redraw();
        }

        /// <summary>
        ///     The Button add point click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            _getClick = !_getClick;
        }

        /// <summary>
        ///     The Button draw lines click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnDrawLines_Click(object sender, RoutedEventArgs e)
        {
            GenerateLine();
        }

        /// <summary>
        ///     The Button draw curves click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The routed event arguments.</param>
        private void BtnDrawCurves_Click(object sender, RoutedEventArgs e)
        {
            // Make a path.
            var path = _vctr.CurveAdd(_points.Values.ToList());

            ImageTest.Children.Add(path);
        }

        private void BtnDrawCube_Click(object sender, RoutedEventArgs e)
        {
            var cube = ResourceObjects.GetCube();
            var translateVector = new Vector3D { X = 50, Y = 50, Z = 0 };
            var angleX = 45;
            var angleY = 45;
            var angleZ = 0;
            var scale = 20;

            var poly = _vctr.LoadObjectFile(cube, translateVector, angleX, angleY, angleZ, scale);

            foreach (var py in GenerateTriangles(poly))
            {
                ImageTest.Children.Add(py);
            }
        }

        private static List<Polygon> GenerateTriangles(Polygons poly)
        {
            var lst = new List<Polygon>();

            for (int i = 0; i < poly.Points.Count; i = i + 3)
            {
                var p = new Polygon();
                p.Points.Add(poly.Points[i]);
                p.Points.Add(poly.Points[i + 1]);
                p.Points.Add(poly.Points[i + 2]);

                lst.Add(p);
            }

            return lst;
        }
    }
}