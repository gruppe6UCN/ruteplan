using Control;
using Database;
using NUnit.Framework;

namespace ControlTest
{
    [TestFixture()]
    class TestExportController
    {
        ExportController ec;
        ImportController ic;

        [SetUp()]
        public void SetUp()
        {
            this.ec = ExportController.Instance;
            this.ic = ImportController.Instance;
        }

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestExportFromFile()
        {
            //string pathRoutes = "Config/RuterCSVTest.csv";
            //string pathStops = "Config/stopsCSV.csv";
            //string pathCustomers = "Config/kunderCSV.csv";
            //ic.ImportFromFile(pathRoutes, pathStops, pathCustomers);
            ec.ExportData();
            Assert.Pass();
        }
    }
}
