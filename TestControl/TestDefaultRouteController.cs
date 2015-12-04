using NUnit.Framework;
using Control;
using Database;
using System.Collections.Generic;
using Model;

namespace ControlTest
{
    [TestFixture()]
    class TestDefaultRouteController
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
            Server.DBClient.Initialize();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetDefaultRoutes()
        {
            List<DefaultRoute> list = drc.GetDefaultRoutes();
            Assert.IsNotEmpty(list);
        }
    }
}
