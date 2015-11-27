using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Database;
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
            Route route = new Route(listDefaultRoute[0], date);
            long routeID = instanceRoute.storeRoute(route);

            //Gets Default Stops
            List<DefaultDeliveryStop> listDefault = instanceDefault.GetDefaultDeliveryStops(84);
            Assert.IsNotEmpty(listDefault);

            //Creates & Stores Stop.
            DeliveryStop stop = new DeliveryStop(listDefault[0]);
            long idTest = instance.StoreDeliveryStop(route.ID, stop);

            //Checks if stored.
            List<Dictionary<String, long>> checkStops = DBConnection.Instance.SendSQL("Select * From DeliveryStop", ConvertMethod);
            Assert.IsNotEmpty(checkStops);

            int count = 0;
            checkStops.ForEach(checkStop =>
            {
                if (checkStop["id"] == idTest)
                    count++;
            });
            Assert.Greater(count, 0);
        }

        private List<Dictionary<String, long>> ConvertMethod(IDataReader dataSet)
        {
            List<Dictionary<String, long>> table = new List<Dictionary<String, long>>();
            while (dataSet.Read())
            {
                Dictionary<String, long> row = new Dictionary<String, long>();
                for (int i = 0; i < dataSet.FieldCount; i++)
                {
                    row[dataSet.GetName(i)] = dataSet.GetInt64(i);
                }
                table.Add(row);
            }
            return table;
        }
    }
}
