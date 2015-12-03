using System;
using System.ServiceModel;
using Model;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using TestWCFService.ServiceRoute;
using WCFService;

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
        public void Test_01_GetRoutes_ExceptionNoList()
        {
            try
            {
                routeClient.GetRoutes();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(FaultException<ExceptionNoRoutes>), e.GetType());
                Assert.AreEqual("No routes are imported.", ((FaultException<ExceptionNoRoutes>)e).Detail.Message);
            }
        }

        [Test()]
        public void Test_02_GetRoutes()
        {
            importClient.Import();
            Route[] routes = routeClient.GetRoutes();
            Assert.NotNull(routes);
            Assert.IsNotEmpty(routes);
        }
    }
}
