using System.Diagnostics;
using System.Linq;
using DataFormatter;
using Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vector3D = Mathematics.Vector3D;

namespace Projections
{
    [TestClass]
    public class Projection
    {
        [TestMethod]
        public void BasicCube()
        {
            var cube = ResourceObjects.GetCube();
            var translateVector = new Vector3D { X = 50, Y = 50, Z = 0 };
            var angleX = 45;
            var angleY = 45;
            var angleZ = 0;
            var scale = 20;
            var tertiary = cube.Select(triangle => Convert(triangle, translateVector, angleX, angleY, angleZ, scale)).ToList();

            foreach (var triangle in tertiary)
            {
                Trace.WriteLine(triangle.ToText());
            }
        }

        private static Vector3D Convert(TertiaryVector triangle, Vector3D translateVector, int angleX, int angleY, int angleZ, int scale)
        {
            var start = new Vector3D {X = triangle.X, Y = triangle.Y, Z = triangle.Z};

            var matrix = Projection3DCamera.WorldMatrix(translateVector, angleX, angleY, angleZ, scale);

            var m = start.To3DMatrix();
            var cache = matrix * m;

            return Projection3D.GetVector(cache);
        }
    }
}
