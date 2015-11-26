﻿using System;
using NUnit.Framework;
using TestWCFService.ServiceRoute;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceDemo
    {
        private IServiceRoute service;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            service = new ServiceRoute.ServiceRouteClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [Test()]
        public void TestGetData()
        {
        }
    }
}