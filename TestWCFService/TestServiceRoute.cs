using System;
using System.ServiceModel;
using Control;
using Model;
using NUnit.Framework;
using TestWCFService.ServiceRoute;

namespace TestWCFService
{
    [TestFixture()]
    class TestServiceRoute
    {
        private ImportController impCtr;
        private ServiceRouteClient routeClient;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            impCtr = ImportController.Instance;
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
            Server.WCFServer.Terminate();
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
            impCtr.ImportRoutes();
            Route[] routes = routeClient.GetRoutes();
            Assert.NotNull(routes);
            Assert.IsNotEmpty(routes);
        }
    }
}
