using Control;
using NUnit.Framework;
using TestWCFService.ServiceExport;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceExport
    {
        private ServiceExportClient clientExport;
        private ImportController impCtr;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.DBClient.Initialize();
            Server.WCFServer.StartServer();
            clientExport = new ServiceExportClient();
            impCtr = ImportController.Instance;
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            clientExport.Close();
            Server.WCFServer.StopServer();
            Server.DBClient.Terminate();
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
            impCtr.ImportRoutes();
            clientExport.Export();
            Assert.Pass();
        }

    }
}
