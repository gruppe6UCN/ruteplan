using System;
using NUnit.Framework;
using TestWCFService.ServiceOptimize;

namespace TestWCFService
{
    [TestFixture()]
    public class TestOptimizeService
    {
        private ServiceOptimizeClient client;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            client = new ServiceOptimizeClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            client.Close();
            Server.WCFServer.StopServer();
            Server.WCFServer.Terminate();
        }

        [Test()]
        public void TestOptimize()
        {
            client.Optimize();
            Assert.Pass();
        }
    }
}
