using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using TestWCFService.ServiceRoute;

namespace TestWCFService
{
    [TestFixture()]
    class TestServiceRoute
    {
        private ServiceImportClient importClient;
        private ServiceRouteClient routeClient;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            importClient = new ServiceImportClient();
            routeClient = new ServiceRouteClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            routeClient.Close();
            Server.WCFServer.StopServer();
        }

        [Test()]
        public void TestGetRoutes_ExceptionNoList()
        {
            importClient.Import();
            Route[] routes = routeClient.GetRoutes();
            Assert.NotNull(routes);
            Assert.IsNotEmpty(routes);
        }
    }
}
