using System;
using NUnit.Framework;
using Model;


namespace TestWCFService
{
    [TestFixture()]
    public class TestDefaultRoute
    {
        DefaultRoute dr;

        [SetUp()]
        public void SetUp()
        {
            dr = new DefaultRoute(TrailerType.STOR, false);
        }

        //Test for TrailerType
        [Test()]
        public void TestTrailerType()
        {
            Assert.AreEqual(dr.TrailerType, TrailerType.STOR);
        }
        //Test for ExtraRoute
        [Test()]
        public void TestExtraRoute()
        {
            Assert.AreEqual(dr.ExtraRoute, false);
        }

    }
}
