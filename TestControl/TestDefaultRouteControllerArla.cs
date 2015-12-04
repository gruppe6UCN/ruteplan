using NUnit.Framework;
using Control;
using Database;
using System.Collections.Generic;
using Model;

namespace ControlTest
{
    [TestFixture()]
    class TestDefaultRouteControllerArla
    {
        DefaultRouteController drc;

        [SetUp()]
        public void SetUp()
        {
            this.drc = DefaultRouteController.Instance;
        }

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.InitializeEmpty();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetDefaultRoutesFromFile()
        {
            string pathRoutes = "Config/RuterCSVTest.csv";
            List<DefaultRoute> list = drc.GetDefaultRoutes(pathRoutes);
            Assert.IsNotEmpty(list);
        }
    }
}
