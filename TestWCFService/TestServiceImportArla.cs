using System;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using Database;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceImportArla
    {
        private ServiceImportClient client;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.InitializeEmpty();
            Server.WCFServer.StartServer();
            client = new ServiceImportClient();
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
        public void TestImportFromArla()
        {
            client.ImportFromArla();
            Assert.Pass();
        }
    }
}
