using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Database;
using Model;

namespace TestServer
{
    [TestFixture()]
    class TestDBGeoLoc
    {
        DBGeoLoc instance;
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

            instance = DBGeoLoc.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetGeoLoc()
        {
            GeoLoc geoLoc = null;
            geoLoc = instance.getGeoLoc(628);
            Assert.IsNotNull(geoLoc);
        }

        [Test()]
        public void TestGetGeoLocs()
        {
            List<GeoLoc> geoLocs = null;
            geoLocs = instance.GetGeoLocs();
            int count = geoLocs.Count;
            Assert.AreEqual(count, 91);
        }
    }
}
