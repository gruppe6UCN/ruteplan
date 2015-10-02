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
        public void TestID()
        {
            Assert.AreEqual(dds.ID, 42);
        }
      

        [Test()]
        public void TestGeoLocID()
        {
            Assert.AreEqual(dds.GeoLocID, 50);
        }
    }

}
