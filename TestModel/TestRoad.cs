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
            ro = new Road();
        }
        //Test 
        [Test()]
        public void TestFrom()
        {
            Assert.AreEqual(ro.from, );
        }
        //Test
        [Test()]
        public void TestTo()
        {
            Assert.AreEqual(ro.to, );
        }
        //Test
        [Test()]
        public void TestDistance()
        {
            Assert.AreEqual(ro.distance, );
        }
        //Test
        [Test()]
        public void TestTime()
        {
            Assert.AreEqual(ro.time, );
        }
               
        
    }
}
