using NUnit.Framework;
using Control;
using Database;
using Model;
using System;
using System.Collections.Generic;

namespace ControlTest
{
    [TestFixture()]
    class TestDeliveryStopController
    {
        DeliveryStopController dsc;
        DBRoute dbr;

        [SetUp()]
        public void SetUp()
        {
            this.dsc = DeliveryStopController.Instance;
            this.dbr = DBRoute.Instance;
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
        public void TestAddDeliveryStops()
        {
            //Creates Routes
            DefaultRoute dRoute = new DefaultRoute(TrailerType.STOR, false);
            Route route = new Route(dRoute, DateTime.Now);
            
            //Creates A Stop
            DefaultDeliveryStop stop = new DefaultDeliveryStop(651, 628);
            List<Customer> customers = new List<Customer>();
            Customer customer = new Customer(22837, "Energivej", "22", 8920, "Randers", new TimeSpan(100));
            customers.Add(customer);
            stop.Customers = customers;

            //Creates List with Stop
            List<DefaultDeliveryStop> list = new List<DefaultDeliveryStop>();
            list.Add(stop);

            //Adds Delivery Stop
            dsc.AddDeliveryStops(route, list);
            Assert.IsNotEmpty(route.Stops);
        }

        [Test()]
        public void TestStoreDeliveryStops()
        {
            //Creates Routes
            DefaultRoute dRoute = new DefaultRoute(TrailerType.STOR, false);
            dRoute.ID = 84;
            Route route = new Route(dRoute, DateTime.Now);

            //Creates A Stop
            DefaultDeliveryStop stop = new DefaultDeliveryStop(651, 628);
            List<Customer> customers = new List<Customer>();
            Customer customer = new Customer(22837, "Energivej", "22", 8920, "Randers", new TimeSpan(100));
            customers.Add(customer);
            stop.Customers = customers;

            //Creates List with Stop
            List<DefaultDeliveryStop> list = new List<DefaultDeliveryStop>();
            list.Add(stop);

            //Adds Delivery Stop
            dsc.AddDeliveryStops(route, list);

            //Store Route
            dbr.storeRoute(route);

            //Stores Stops
            dsc.StoreDeliveryStops(route);
            Assert.Pass();
        }
    }
}
