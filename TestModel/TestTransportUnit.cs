using NUnit.Framework;
using System;
using Model;

namespace TestModel
{
    [TestFixture()]
    public class TestTransportUnit
    {
        TransportUnit tu;

        [SetUp()]
        public void SetUp()
        {
            tu = new TransportUnit(42, 1337, 3.14);
        }

        [Test()]
        public void TestID()
        {
            Assert.AreEqual(tu.ID, 42);
        }

        [Test()]
        public void TestCustomerID()
        {
            Assert.AreEqual(tu.CustomerID, 1337);
        }

        [Test()]
        public void TestUnitType()
        {
            Assert.AreEqual(tu.UnitType, 3.14);
        }
    }
}

