using System;
using System.Collections.Generic;
using NUnit.Framework;
using Model;

namespace TestModel
{
    [TestFixture()]
    public class TestRoad
    {
        Road ro;

        [SetUp()]
        public void Setup()
        {
            ro = new Road(1, 2, 400, new TimeSpan(12, 0, 0));
        }
        //Test 
        [Test()]
        public void TestOriginalConstructor()
        {
            Assert.AreEqual(ro.From, 1);
            Assert.AreEqual(ro.To, 2);
            Assert.AreEqual(ro.Distance, 400);
            Assert.AreEqual(ro.Time.Hours, 12);
        }

        [Test()]
        public void TestNewConstructor()
        {
            ro.FromGeoLog = new GeoLoc(11, 0, 0);
            Assert.AreEqual(ro.From, 11);
            Assert.AreEqual(ro.FromGeoLog.ID, 11);
            ro.ToGeoLog = new GeoLoc(22, 0, 0);
            Assert.AreEqual(ro.To, 22);
            Assert.AreEqual(ro.ToGeoLog.ID, 22);
        }

        [Test()]
        public void TestEqual()
        {
            Road ro1 = new Road(new GeoLoc(1, 0, 0), new GeoLoc(2, 0, 0), 400, new TimeSpan(12, 0, 0));
            Road ro2 = new Road(new GeoLoc(11, 0, 0), new GeoLoc(22, 0, 0), 400, new TimeSpan(12, 0, 0));
            Assert.IsTrue(ro.Equals(ro1));
            Assert.IsFalse(ro.Equals(ro2));

            Road ro3 = new Road(11, 2, 400, new TimeSpan(12, 0, 0));
            Assert.IsFalse(ro.Equals(ro3));
            ro3.FromGeoLog = new GeoLoc(1, 0, 0);
            Assert.IsTrue(ro.Equals(ro3));
            ro3.FromGeoLog = new GeoLoc(11, 0, 0);
            Assert.IsFalse(ro.Equals(ro3));

            Road ro4 = new Road(1, 22, 400, new TimeSpan(12, 0, 0));
            Assert.IsFalse(ro.Equals(ro4));
            ro4.ToGeoLog = new GeoLoc(2, 0, 0);
            Assert.IsTrue(ro.Equals(ro4));
            ro4.FromGeoLog = new GeoLoc(22, 0, 0);
            Assert.IsFalse(ro.Equals(ro4));
        }
        
    }
}
