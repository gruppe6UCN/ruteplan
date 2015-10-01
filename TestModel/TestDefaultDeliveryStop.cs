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
            dds = new DefaultDeliveryStop();
        }

        [Test()]
        public void TestID()
        {
            Assert.AreEqual(dds.ID, );
        }
      

        [Test()]
        public void TestGeoLocID(dds.GeoLocID, )
        {
            Assert.AreEqual();
        }
    }

}
