using System;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using Database;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceImport
    {
        private ServiceImportClient client;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
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
        public void TestImport()
        {
            client.Import();
            Assert.Pass();
        }

        [Test()]
        public void TestImportFromArla()
        {
            DBConnection.Instance.DB = "TestArlaEmpty";
            client.ImportFromArla();
            Assert.Pass();
        }
    }
}
