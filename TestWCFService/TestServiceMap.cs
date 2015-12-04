using System.Collections.Generic;
using Control;
using GMap.NET;
using Model;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using TestWCFService.ServiceMap;
using TestWCFService.ServiceOptimize;
using TestWCFService.ServiceRoute;
using WCFWrapper;

namespace TestWCFService
{
    [TestFixture()]
    internal class TestServiceMap
    {
        private ServiceImportClient serviceImport;
        private ServiceOptimizeClient serviceOptimize;
        private ServiceRouteClient serviceRoute;
        private ServiceMapClient serviceMap;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            serviceImport = new ServiceImportClient();
            serviceOptimize = new ServiceOptimizeClient();
            serviceRoute = new ServiceRouteClient();
            serviceMap = new ServiceMapClient();
            serviceImport.Import();
            serviceOptimize.Optimize();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            Server.WCFServer.StopServer();
            Server.WCFServer.Terminate();
            serviceImport.Close();
            serviceOptimize.Close();
            serviceRoute.Close();
            serviceMap.Close();
        }

        [Test()]
        public void TestGetRoadMap()
        {
            Route route = serviceRoute.GetRoutes()[0];
            MapRouteWrapper roadMap = serviceMap.GetRoadMap(route);
            List<MapRoute> mapRoutes = roadMap.UnWrab();
            Assert.IsNotEmpty(mapRoutes);
        }

        [Test()]
        public void TestGetRoadMap_OrderOfRoutesAndPoints()
        {
            Route[] routes = serviceRoute.GetRoutes();

            foreach (Route route in routes)
            {
                // Calls the MapController direct to get original List<MapRoute> for comparacy
                List<MapRoute> originalMapRoute = MapController.Instance.GetCalcRoad(route);
                // Wrap and unwrap the original originalMapRoute for comparacy
                List<MapRoute> wrapperMapRoute = new MapRouteWrapper(originalMapRoute).UnWrab();

                for (int i = 0; i < originalMapRoute.Count; i++)
                {
                    for (int ii = 0; ii < originalMapRoute[i].Points.Count; ii++)
                    {
                        Assert.AreEqual(originalMapRoute[i].Points[ii].Lat, wrapperMapRoute[i].Points[ii].Lat);
                        Assert.AreEqual(originalMapRoute[i].Points[ii].Lng, wrapperMapRoute[i].Points[ii].Lng);
                    }
                }
            }
        }
    }
}
