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
            r = new Route(dr, new DateTime(1995, 01, 28), new DateTime(12, 3, 4));
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
            DateTime testTime = new DateTime(1995, 01, 28);
            Assert.AreEqual(r.DateForDeparture.Date, testTime);  
        }
                  
    }
}
