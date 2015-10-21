using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Model;

namespace TestServer
{
    class TestDBDefaultRoute
    {
        DBDefaultRoute instance;
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {

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

            instance = DBDefaultRoute.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestDefaultRoutes()
        {
            List<DefaultRoute> routes = instance.DefaultRoutes();
            Assert.IsNotEmpty(routes);

            DefaultRoute route = null;
            try
            {
                route = routes.First(r =>
                {
                    return (r.ID == 84 && r.TrailerType == TrailerType.STOR && r.ExtraRoute == false);
                });
            }
            catch (InvalidOperationException e) { }
            Assert.IsNotNull(route);
        }

        [Test()]
        public void TestDefaultRoutes_isNotEmpty()
        {
            List<DefaultRoute> routes = instance.DefaultRoutes();
            Assert.IsNotEmpty(routes);
        }


    }
}
