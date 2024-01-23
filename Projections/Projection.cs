using System.Collections.Generic;
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
        public void Basic3D()
        {
            //1 Radian
            var angleX =0;
            var angleY = 0;
            var angleZ = 0;

            var vector = new Vector3D()
            {
                X = 0,
                Y = 0,
                Z = 0
            };

            var translate = new Vector3D { X = 0, Y = 0, Z = 3 };

            var compare = CorrectData(vector, translate,angleX, angleY, angleZ, 1);
            //0,0,3
            //0,0,0.96676334250565255

            var world = World(vector, 0, translate);

            Assert.IsTrue(compare.Equals(world), "Not Equal");
        }


        [TestMethod]
        public void BasicCube()
        {
            var cube = ResourceObjects.GetCube();
            //1 Radian
            var translateVector = new Vector3D { X = 1, Y = 1, Z = 1 };
            var angleX = 57.296;
            var angleY =0;
            var angleZ = 57.296;
            var scale = 1;

            var lstOld = cube.Select(tertiary => new Vector3D {X = tertiary.X, Y = tertiary.Y, Z = tertiary.Z}).Select(vector => CorrectData(vector, translateVector, angleX, angleY, angleZ, scale)).ToList();

            var lstNew = cube.Select(tertiary => Convert(tertiary, translateVector, angleX, angleY, angleZ, scale)).ToList();

            for (int i = 0; i < lstOld.Count; i++)
            {
                var compare = lstOld[i];
                var world = lstNew[i];

                Assert.IsTrue(compare.Equals(world), "Not Equal");
            }
        }

        private static Vector3D CorrectData(Vector3D vector, Vector3D translator, double angleX, double angleY, double angleZ, int scale)
        {
            var m = Projection3D.RotateZ(vector, angleZ);
            vector = m.MatrixTo3DVector();
            m = Projection3D.RotateY(vector, angleY);
            vector = m.MatrixTo3DVector();
            m = Projection3D.RotateX(vector, angleX);
            vector = m.MatrixTo3DVector();
            m = Projection3D.Translate(vector, translator);
            vector = m.MatrixTo3DVector();
            m = Projection3D.Scale(vector, scale);
            vector = m.MatrixTo3DVector();
            return Projection3DCamera.ProjectionTo3D(vector);
        }

        private static Vector3D World(Vector3D vector, double angle, Vector3D translator)
        {
            return Projection3DCamera.WorldMatrix(vector, translator, angle, 0, angle, 1);
        }

        private static Vector3D Convert(TertiaryVector triangle, Vector3D translateVector, double angleX, double angleY, double angleZ, int scale)
        {
            var start = new Vector3D {X = triangle.X, Y = triangle.Y, Z = triangle.Z};

            return Projection3DCamera.WorldMatrix(start, translateVector, angleX, angleY, angleZ, scale);
        }
    }
}
