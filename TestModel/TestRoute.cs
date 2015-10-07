using System;
using NUnit.Framework;
using Model;

namespace TestModel
{
    [TestFixture()]

    public class TestRoute
    {
        Route r;
        DefaultRoute dr;

        [SetUp()]
        public void SetUp()
        {
            dr = new DefaultRoute(1, false);
            r = new Route(dr, new DateTime(1200));
        }
        //Test for DefaultRoute
        [Test()]
        public void TestDefaultRoute()
        {
            Assert.AreEqual(r.DefaultRoute, dr);
        }
        //Test for DateOfDeparture
        [Test()]
        public void TestDate()
        {
            Assert.AreEqual(r.DateForDeparture.Ticks, 1200);
        }
                  
    }
}
