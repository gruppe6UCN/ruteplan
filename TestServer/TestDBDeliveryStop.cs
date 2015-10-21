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

            instance = DBDeliveryStop.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestStoreDeliveryStop()
        {
            
            
            
            
            
            DateTime time = new DateTime();
            DefaultRoute defaultRoute = new DefaultRoute(TrailerType.STOR, true);
            Route route = new Route(defaultRoute, time);
            DefaultDeliveryStop defaultStop = new DefaultDeliveryStop(84, 628);
            DeliveryStop stop = new DeliveryStop(defaultStop);
            instance.StoreDeliveryStop(route.ID, stop);
            
            


            Assert.
        }
    }
}
