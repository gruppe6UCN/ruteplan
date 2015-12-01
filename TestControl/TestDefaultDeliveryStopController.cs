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
            Server.WCFServer.Initialize();
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

        [Test()]
        public void TestGetDefaultDeliveryStopsFromFile()
        {
            string pathStops = "Config/stopsCSV.csv";
            string pathCustomers = "Config/kunderCSV.csv";
            ddsc.ImportDefaultDeliveryStopsFromFile(pathStops, pathCustomers);
            
            DefaultRoute route = new DefaultRoute(TrailerType.STOR, false);
            route.ID = 350;
            List<DefaultDeliveryStop> stops = ddsc.GetDefaultDeliveryStopsFromFile(route);
            Assert.IsNotEmpty(stops);
        }

        [Test()]
        public void TestImportDefaultDeliveryStopsFromFile()
        {
            string pathStops = "Config/stopsCSV.csv";
            string pathCustomers = "Config/kunderCSV.csv";
            ddsc.ImportDefaultDeliveryStopsFromFile(pathStops, pathCustomers);
            Assert.IsNotEmpty(ddsc.TmpDefaultStops);
        }

    }
}
