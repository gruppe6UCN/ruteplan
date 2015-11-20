using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Control;
using GMap.NET;
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
            RouteController.Instance.ImportRoutes(new DateTime(2015, 11, 19));
            List<Route> routes = RouteController.Instance.Routes.ToList();

            MapRoute mapRoute = MapController.ShortestDistancesTo(
                routes[3].Stops[routes[3].Stops.Count-1], routes[1]);

            Assert.NotNull(mapRoute.Distance);
            Assert.AreNotEqual(0.0, mapRoute.Distance);
        }
    }
}
