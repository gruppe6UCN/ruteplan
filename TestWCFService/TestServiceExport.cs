using System;
using NUnit.Framework;
using TestWCFService.ServiceExport;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceExport
    {
        private ServiceExportClient client;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            client = new ServiceExportClient();
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
        public void TestExport()
        {
            client.Export();
            Assert.Pass();
        }

    }
}
