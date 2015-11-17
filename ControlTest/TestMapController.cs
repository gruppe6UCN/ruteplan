using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Control;
using Model;
using Server;
using Server.Database;

namespace ControlTest
{
    [TestFixture()]
    class TestMapController
    {
        MapController mc;
        DBConnection dbConnection;
        DBGeoLoc dbGeoLoc;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            this.mc = MapController.Instance;
            this.dbConnection = DBConnection.Instance;
            dbConnection.Host = "localhost";
            dbConnection.DB = "TestArla";
            dbConnection.User = File.ReadAllText("Config/user.txt");
            dbConnection.Pass = File.ReadAllText("Config/pass.txt");
            dbConnection.Connect();
            this.dbGeoLoc = DBGeoLoc.Instance;
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            this.dbConnection.Disconnect();
        }

        [Test()]
        public void TestFindShortestDistance()
        {
            List<GeoLoc> geoLocs = dbGeoLoc.GetGeoLocs();
            List<Road> shortestDistance = MapController.ShortestDistances(geoLocs[3], geoLocs);

            double shortest = 0.0;
            foreach (Road key in shortestDistance)
            {
                Assert.LessOrEqual(shortest, key.Distance);
                shortest = key.Distance;
            }
            Assert.Pass();
        }
    }
}
