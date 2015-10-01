using System;
using NUnit.Framework;
using Model;

namespace TestModel
{
    [TestFixture()]

    public class TestDeliveryStop
    {
        DeliveryStop  ds;

        [SetUp()]
        public void SetUp()
        {
            ds = new DeliveryStop();
        }

        //Test for DefaultStop 
        [Test()]
        public void TestDefaultDeliveryStop()
        {
            Assert.AreEqual(ds.defaultStop, );
        }
    }
}
