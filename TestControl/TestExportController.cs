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
            Server.DBClient.Initialize();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestExport()
        {
            ic.ImportRoutes();
            ec.ExportData();
            Assert.Pass();
        }
    }
}
