using System;
using NUnit.Framework;
using TestWCFService.ServiceExport;
using TestWCFService.ServiceImport;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceExport
    {
        private ServiceExportClient clientExport;
        private ServiceImportClient clientImport;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            clientExport = new ServiceExportClient();
            clientImport = new ServiceImportClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            clientExport.Close();
            clientImport.Close();
            Server.WCFServer.StopServer();
            Server.WCFServer.Terminate();
        }

        [Test()]
        public void TestExportNothing()
        {
            clientExport.Export();
            Assert.Pass();
        }

        [Test()]
        public void TestExportData()
        {
            clientImport.Import();
            clientExport.Export();
            Assert.Pass();
        }

    }
}
