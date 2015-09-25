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
            dr = new DefaultRoute();
        }

        //Test for ID
        [Test()]
        public void TestID()
        {
            Assert.AreEqual(dr.id, );
        }
        //Test for TrailerType
        public void TestTrailerType()
        {
            Assert.AreEqual(dr.trailerType, );
        }
        //Test for ExtraRoute
        public void TestExtraRoute()
        {
            Assert.AreEqual(dr.extraRoute, );
        }

    }
}
