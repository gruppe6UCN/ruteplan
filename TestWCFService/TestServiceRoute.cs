﻿using System;
using System.ServiceModel;
using Model;
using NUnit.Framework;
using TestWCFService.ServiceImport;
using TestWCFService.ServiceRoute;
using WCFService;

namespace TestWCFService
{
    [TestFixture()]
    class TestServiceRoute
    {
        private ServiceImportClient importClient;
        private ServiceRouteClient routeClient;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            importClient = new ServiceImportClient();
            routeClient = new ServiceRouteClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            routeClient.Close();
            Server.WCFServer.StopServer();
        }

        [Test()]
        public void TestGetRoutes_01_ExceptionNoList()
        {
            try
            {
                routeClient.GetRoutes();
                Assert.Fail();
            }
            catch (FaultException<ExceptionNoRoutes> e)
            {
                Assert.AreEqual("No routes is imported.", e.Detail.Message);
            }
        }

        [Test()]
        public void TestGetRoutes_02()
        {
            importClient.Import();
            Route[] routes = routeClient.GetRoutes();
            Assert.NotNull(routes);
            Assert.IsNotEmpty(routes);
        }
    }
}
