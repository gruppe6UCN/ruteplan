using System.Collections.Generic;
using GMap.NET;
using Model;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using TestWCFService.ServiceMap;
using TestWCFService.ServiceOptimize;
using TestWCFService.ServiceRoute;
using WCFService;

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
            List<MapRoute> mapRoutes = roadMap.GetMapRoute();
            Assert.Pass();
        }
    }
}
