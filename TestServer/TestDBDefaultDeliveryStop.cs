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
    class TestDBDefaultDeliveryStop
    {
        DBDefaultDeliveryStop instance;
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {

            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            } catch { }

            DBConnection.Instance.Host = "localhost";
            DBConnection.Instance.DB = "TestArla";
            DBConnection.Instance.User = user;
            DBConnection.Instance.Pass = pass;

            DBConnection.Instance.Connect();

            instance = DBDefaultDeliveryStop.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetDefaultDeliveryStop()
        {
            List<DefaultDeliveryStop> stops = instance.GetDefaultDeliveryStops(84);
            Assert.IsNotEmpty(stops);
            Assert.AreEqual(stops[0].ID, 651);
            Assert.AreEqual(stops[1].ID, 1192);
            Assert.AreEqual(stops[0].GeoLocID, 84);
            Assert.AreEqual(stops[1].GeoLocID, 84);
        }

        [Test()]
        public void TestGetDefaultDeliveryStop_IsEmpty()
        {
            List<DefaultDeliveryStop> stops = instance.GetDefaultDeliveryStops(0);
            Assert.IsEmpty(stops);
        }
    }
}
