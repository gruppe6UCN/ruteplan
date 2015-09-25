using System;
using NUnit.Framework;
using Model;

namespace TestModel
{
    [TestFixture()]

    public class TestRoute
    {
        Route r;

        [SetUp()]
        public void SetUp()
        {
            r = new Route();
        }
        //Test for DefaultRoute
        [Test()]
        public void TestDefaultRoute()
        {
            Assert.AreEqual(r.DefaultRoute, );
        }
        //Test for DateOfDeparture
        [Test()]
        public void TestDate()
        {
            Assert.AreEqual(r.DateForDeparture, );
        }
                  
    }
}
