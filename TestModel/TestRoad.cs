using System;
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
            ro = new Road(1, 2, 400, new DateTime(12));
        }
        //Test 
        [Test()]
        public void TestFrom()
        {
            Assert.AreEqual(ro.From, 1);
        }
        //Test
        [Test()]
        public void TestTo()
        {
            Assert.AreEqual(ro.To, 2);
        }
        //Test
        [Test()]
        public void TestDistance()
        {
            Assert.AreEqual(ro.Distance, 400);
        }
        //Test
        [Test()]
        public void TestTime()
        {
            Assert.AreEqual(ro.Time, 12);
        }
               
        
    }
}
