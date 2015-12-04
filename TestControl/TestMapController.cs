using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Control;
using NUnit.Framework;
using Database;
using GMap.NET;
using Model;

namespace ControlTest
{
    [TestFixture()]
    class TestMapController
    {
        private MapController mapCtr;
        private RouteController routeCtr;
        private ImportController importCtr;
        private DBConnection dbConnection;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            mapCtr = MapController.Instance;
            routeCtr = RouteController.Instance;
            importCtr = ImportController.Instance;
            dbConnection = DBConnection.Instance;
            dbConnection.Host = "localhost";
            dbConnection.DB = "TestArla";
            dbConnection.User = File.ReadAllText("Config/user.txt");
            dbConnection.Pass = File.ReadAllText("Config/pass.txt");
            dbConnection.Connect();
            importCtr.ImportRoutes();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            dbConnection.Disconnect();
        }

        [Test()]
        public void TestFindShortestDistance()
        {
            RouteController.Instance.ImportRoutes(new DateTime(2015, 11, 19));
            List<Route> routes = routeCtr.Routes.ToList();

            MapRoute mapRoute = MapController.ShortestDistancesTo(
                routes[3].Stops[routes[3].Stops.Count - 1], routes[1]);

            Assert.NotNull(mapRoute.Distance);
            Assert.AreNotEqual(0.0, mapRoute.Distance);
        }

        [Test()]
        public void TestPreCalcRoad()
        {
            Stopwatch preTime = new Stopwatch();
            Stopwatch getTime = new Stopwatch();

            Route route = routeCtr.Routes.First(r => r.DefaultRoute.ID == 121);

            preTime.Start();
            mapCtr.PreCalcRoad(route);
            preTime.Stop();

            getTime.Start();
            mapCtr.GetCalcRoad(route);
            getTime.Stop();

            Assert.Greater(preTime.ElapsedTicks, getTime.ElapsedTicks);
        }

        [Test()]
        public void TestGetCalcRoad()
        {
            Stopwatch firstTime = new Stopwatch();
            Stopwatch secondTime = new Stopwatch();

            Route route = routeCtr.Routes.First(r => r.DefaultRoute.ID == 84);

            firstTime.Start();
            mapCtr.GetCalcRoad(route);
            firstTime.Stop();

            secondTime.Start();
            mapCtr.GetCalcRoad(route);
            secondTime.Stop();

            Assert.Greater(firstTime.ElapsedTicks, secondTime.ElapsedTicks);
        }
    }
}
