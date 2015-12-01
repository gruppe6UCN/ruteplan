using System;
using System.Collections.Generic;
using System.ServiceModel;
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
            importClient.Open();
            routeClient.Open();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            routeClient.Close();
            importClient.Close();
            Server.WCFServer.StopServer();
        }

        [Test()]
        public void TestGetRoutes_01_ExceptionNoList()
        {
            try
            {
                routeClient.GetRoutes();
                Assert.Fail();
            }
            catch (FaultException<ExceptionNoRoutes> e)
            {
                Assert.AreEqual("No routes is imported.", e.Detail.Message);
            }
        }

        [Test()]
        public void TestGetRoutes_02()
        {
            importClient.Import();
            List<Route> routes = routeClient.GetRoutes();
            Assert.NotNull(routes);
            Assert.IsNotEmpty(routes);
        }
    }
}
