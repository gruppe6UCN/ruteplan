using NUnit.Framework;
using Control;
using Database;
using Model;
using System;
using System.Collections.Generic;

namespace ControlTest
{
    [TestFixture()]
    class TestDefaultDeliveryStopController
    {
        DefaultDeliveryStopController ddsc;

        [SetUp()]
        public void SetUp()
        {
            this.ddsc = DefaultDeliveryStopController.Instance;
        }

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.DBClient.Initialize();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetDefaultDeliveryStops()
        {
            DefaultRoute route = new DefaultRoute(TrailerType.STOR, false);
            route.ID = 84;
            List<DefaultDeliveryStop> stops = ddsc.GetDefaultDeliveryStops(route);
            Assert.IsNotEmpty(stops);
        }
    }
}
