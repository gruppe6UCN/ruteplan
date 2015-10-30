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
    class TestDBDeliveryStop
    {
        DBRoute instanceRoute;
        DBDefaultRoute instanceDefaultRoute;
        DBDefaultDeliveryStop instanceDefault;
        DBDeliveryStop instance;
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

            instanceRoute = DBRoute.Instance;
            instanceDefaultRoute = DBDefaultRoute.Instance;
            instanceDefault = DBDefaultDeliveryStop.Instance;
            instance = DBDeliveryStop.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        //TODO: Fix test...
        public void TestStoreDeliveryStop()
        {
            //Gets Default Routes.
            List<DefaultRoute> listDefaultRoute = instanceDefaultRoute.DefaultRoutes();
            Assert.IsNotEmpty(listDefaultRoute);

            //Creates & Stores a route.
            DateTime date = new DateTime(1995, 01, 28);
            DateTime time = new DateTime(12, 3, 4);
            Route route = new Route(listDefaultRoute[0], date, time);
            long routeID = instanceRoute.storeRoute(route);

            //Gets Default Stops
            List<DefaultDeliveryStop> listDefault = instanceDefault.GetDefaultDeliveryStops(84);
            Assert.IsNotEmpty(listDefault);

            //Creates & Stores Stop.
            DeliveryStop stop = new DeliveryStop(listDefault[0]);
            long idTest = instance.StoreDeliveryStop(route.ID, stop);

            //Checks if stored.
            List<DeliveryStop> list = instance.GetDeliveryStops(listDefault);
            Assert.IsNotEmpty(list);
            DeliveryStop stopTest = null;
            try
            {
                stopTest = list.First(r =>
                {
                    return (r.ID == idTest);
                });
            }
            catch (InvalidOperationException e) { }
            Assert.IsNotNull(stopTest);
        }
    }
}
