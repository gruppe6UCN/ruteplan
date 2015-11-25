using System;
using NUnit.Framework;
using Model;

namespace TestWCFService
{
    [TestFixture()]
    public class TestGeoLoc
    {
        GeoLoc gl;

        [SetUp()]
        public void SetUp()
        {
            gl = new GeoLoc(42, 57.01874, 9.882112); // Hal9k
        }

        [Test()]
        public void TestConstructor()
        {
            Assert.AreEqual(gl.ID, 42);
            Assert.AreEqual(gl.Location.Latitude, 57.01874);
            Assert.AreEqual(gl.Location.Longitude, 9.882112);
        }

        [Test()]
        public void TestFliedDistance()
        {
            double d = 6.380; //distance between Hal9k and De-Klubben
            GeoLoc gl2 = new GeoLoc(1337, 57.01380, 9.987038); // De-Klubben
            double sum = gl.FliedDistance(gl2);
            Assert.IsTrue(d - 1 < sum && d + 1 > sum);
        }

        [Test()]
        public void TestPoint()
        {
            Assert.AreEqual(gl.Location.Latitude, gl.Point.Lat);
            Assert.AreEqual(gl.Location.Longitude, gl.Point.Lng);
        }
    }
}

