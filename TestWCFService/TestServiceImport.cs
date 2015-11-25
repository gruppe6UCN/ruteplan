using System;
using NUnit.Framework;
using TestWCFService.ServiceImport;

namespace TestWCFService
{
    [TestFixture()]
    public class TestServiceImport
    {
        private IServiceImport service;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            service = new ServiceImport.ServiceImportClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [Test()]
        public void TestImport()
        {
            service.Import();
            Assert.Pass();
        }
    }
}
