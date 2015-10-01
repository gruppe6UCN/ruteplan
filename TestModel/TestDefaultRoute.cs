using System;
using NUnit.Framework;
using Model;


namespace TestModel
{
    [TestFixture()]

    public class TestDefaultRoute
    {
        DefaultRoute dr;

        [SetUp()]
        public void SetUp()
        {
            dr = new DefaultRoute(4, false);
        }


        //Test for TrailerType
        public void TestTrailerType()
        {
            Assert.AreEqual(dr.TrailerType, 4);
        }
        //Test for ExtraRoute
        public void TestExtraRoute()
        {
            Assert.AreEqual(dr.ExtraRoute, false);
        }

    }
}
