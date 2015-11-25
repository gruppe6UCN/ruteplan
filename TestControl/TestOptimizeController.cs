using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Control;
using Model;
using NUnit.Framework.Constraints;
using Server;
using TestServer;

namespace ControlTest
{
    [TestFixture()]
    class TestOptimizeController
    {
        OptimizeController oc;
        private ConcurrentBag<Route> routes;
        private Route route1;
        private Route route2;
        private DeliveryStop d3;
        private DeliveryStop d6;
        private DeliveryStop d9;
        private DeliveryStop d12;
        private DeliveryStop d15;
        private DeliveryStop d18;
        private DeliveryStop d55;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            this.oc = OptimizeController.Instance;
        }

        [SetUp()]
        public void SetUp()
        {
            routes = new ConcurrentBag<Route>();

            route1 = new Route(new DefaultRoute(100, TrailerType.STOR, false), DateTime.Now);

            d3 = new DeliveryStop(new DefaultDeliveryStop(10000 + 1, 0 + 1));
            d3.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 1, 30 + 1, 3 * 1) };
            route1.Stops.Add(d3);
            d6 = new DeliveryStop(new DefaultDeliveryStop(10000 + 2, 0 + 2));
            d6.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 2, 30 + 2, 3 * 2) };
            route1.Stops.Add(d6);
            d9 = new DeliveryStop(new DefaultDeliveryStop(10000 + 3, 0 + 3));
            d9.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 3, 30 + 3, 3 * 3) };
            route1.Stops.Add(d9);
            d12 = new DeliveryStop(new DefaultDeliveryStop(10000 + 4, 0 + 4));
            d12.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 4, 30 + 4, 3 * 4) };
            route1.Stops.Add(d12);
            d15 = new DeliveryStop(new DefaultDeliveryStop(10000 + 5, 0 + 5));
            d15.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 5, 30 + 5, 3 * 5) };
            route1.Stops.Add(d15);
            d18 = new DeliveryStop(new DefaultDeliveryStop(10000 + 6, 0 + 6));
            d18.TransportUnits = new List<TransportUnit>() { new TransportUnit(100 + 6, 30 + 6, 3 * 6) };
            route1.Stops.Add(d18);


            route2 = new Route(new DefaultRoute(100, TrailerType.STOR, false), DateTime.Now);
            d55 = new DeliveryStop(new DefaultDeliveryStop(10000 + 7, 0 + 7));
            d55.TransportUnits = new List<TransportUnit>();
            for (int i = 0; i < 40; i++)
            {
                d55.TransportUnits.Add(new TransportUnit(300 + i, 300 + i, 1.8));
            }
            for (int i = 0; i < 60; i++)
            {
                d55.TransportUnits.Add(new TransportUnit(300 + i, 300 + i, 1));
            }
            route2.Stops.Add(d55);
        }

        [Test()]
        public void TestFindBestOverloadedStop()
        {
            List<DeliveryStop> stops = OptimizeController.FindAndRemoveStops(route1, routes);
            Assert.Contains(d9, stops);
            Assert.Contains(d3, stops);
        }

        [Test()]
        public void TestFindBestOverloadedStop_AddExtraRoute()
        {
            List<DeliveryStop> stops = OptimizeController.FindAndRemoveStops(route2, routes);
            Assert.AreEqual(0, stops.Count);
            Assert.AreEqual(1, route2.Stops.Count);
            foreach (TransportUnit transportUnit in route2.Stops[0].TransportUnits)
            {
                Assert.AreEqual(1, transportUnit.UnitType);
            }
            Assert.AreEqual(2, routes.Count);
            foreach (Route route in routes)
            {
                Assert.LessOrEqual(route.GetCapacity(), route.GetCapacity());
                Assert.Greater(route.GetCapacity(), route.GetCapacity()-1);
            }
        }
    }
}
