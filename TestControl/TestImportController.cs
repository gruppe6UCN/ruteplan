using System;
using System.IO;
using NUnit.Framework;
using Control;
using Database;


namespace ControlTest
{
    [TestFixture()]
    public class TestImportController
    {
        ImportController ic;
        RouteController rc;
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            ic = ImportController.Instance;
            rc = RouteController.Instance;

            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            }
            catch { }

            DBConnection.Instance.Host = "localhost";
            DBConnection.Instance.DB = "TestArla";
            DBConnection.Instance.User = user;
            DBConnection.Instance.Pass = pass;
            DBConnection.Instance.Connect();
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestImportRoutes()
        {
            ic.ImportRoutes();
            Assert.IsNotEmpty(rc.Routes);
        }
    }
}
