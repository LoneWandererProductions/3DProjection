using System.Collections.Generic;
using DataFormatter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projections
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicCube()
        {
            var cube = ResourceObjects.GetCube();
        }

    }
}
