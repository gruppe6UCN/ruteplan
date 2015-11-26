using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestWCFService.ServiceRoute;

namespace TestWCFService
{
    [TestFixture()]
    class TestServiceRoute
    {
        private ServiceRouteClient client;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.Program.StartServer();
            client = new ServiceRouteClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            client.Close();
            Server.Program.StopServer();
        }

        [Test()]
        public void TestGetRoutes_ExceptionNoList()
        {
            Route route = client.GetRoutes();
            Assert.NotNull(route);
            Assert.AreEqual(21, route.ID);
        }
    }
}
