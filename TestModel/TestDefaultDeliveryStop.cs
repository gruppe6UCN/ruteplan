using System;
using NUnit.Framework;
using Model;


namespace TestModel
{
    [TestFixture()]
    public class TestDefaultDeliveryStop
    {
        DefaultDeliveryStop dds;

        [SetUp()]
        public void SetUp()
        {
            dds = new DefaultDeliveryStop(42, 50);
        }

        [Test()]
        public void TestConstructor()
        {
            Assert.AreEqual(dds.ID, 42);
            Assert.AreEqual(dds.GeoLocID, 50);
        }

        [Test()]
        public void TestGeoLog()
        {
            Assert.AreEqual(dds.GeoLocID, 50);
            dds.GeoLoc = new GeoLoc(1337, 0.0, 0.0);
            Assert.AreEqual(dds.GeoLocID, 1337);
            dds.GeoLoc = null;
            Assert.AreEqual(dds.GeoLocID, 50);
            dds.GeoLoc = new GeoLoc(50, 0.0, 0.0);
            Assert.AreEqual(dds.GeoLocID, 50);
        }
    }

}
