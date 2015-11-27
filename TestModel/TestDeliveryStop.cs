using System;
using NUnit.Framework;
using Model;

namespace TestModel
{
    [TestFixture()]

    public class TestDeliveryStop
    {
        DeliveryStop  ds;
        DefaultDeliveryStop dds;

        [SetUp()]
        public void SetUp()
        {
            dds = new DefaultDeliveryStop(2, 300);
            ds = new DeliveryStop(dds);
        }

        //Test for DefaultStop 
        [Test()]
        public void TestDefaultDeliveryStop()
        {
            Assert.AreEqual(ds.DefaultStop, dds);
        }
    }
}
