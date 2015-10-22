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
    [TestFixture()]
    class TestDBRoute
    {
        DBRoute instance;
        DBDefaultRoute instanceDefaultRoute;
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

            instance = DBRoute.Instance;
            instanceDefaultRoute = DBDefaultRoute.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        //TODO: Fix test...
        public void TestStoreRoute()
        {
            //Gets default routes.
            List<DefaultRoute> defaultRoutes = instanceDefaultRoute.DefaultRoutes();
            Assert.IsNotEmpty(defaultRoutes);

            //Creates a route.
            DateTime date = new DateTime(1995, 01, 28, 12, 3, 4);
            Route route = new Route(defaultRoutes[0], date);

            //Stores route.
            long routeID = instance.storeRoute(route);
            
            //Checks if stored.
            List<Route> routes = instance.GetRoutes(defaultRoutes);
            Assert.IsNotEmpty(routes);
            Route testRoute = null;
            try
            {
                testRoute = routes.First(r =>
                {
                    return (r.ID == routeID);
                });
            }
            catch (InvalidOperationException e) { }
            Assert.IsNotNull(testRoute);
        }
    }
}
