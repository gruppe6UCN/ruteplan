using System;
using NUnit.Framework;
using TestWCFService.ServiceOptimize;
using System.Threading.Tasks;

namespace TestWCFService
{
    [TestFixture()]
    public class TestOptimizeService
    {
        private ServiceOptimizeClient client;
        private ServiceOptimizeClient client2;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.WCFServer.Initialize();
            Server.WCFServer.StartServer();
            client = new ServiceOptimizeClient();
            client2 = new ServiceOptimizeClient();
        }

        [SetUp()]
        public void SetUp()
        {
        }

        [TestFixtureTearDown()]
        public void ClassTeardown()
        {
            client.Close();
            client2.Close();
            Server.WCFServer.StopServer();
            Server.WCFServer.Terminate();
        }

        [Test()]
        public void TestOptimize()
        {
            client.Optimize();
            Assert.Pass();
        }

        [Test()]
        public async void TestDoubleOptimize()
        {
            var c1 = Task.Run(() => Optimize(client));
            var c2 = Task.Run(() => Optimize(client2));

            await c1;
            await c2;

            Assert.Pass();
        }

        [Test()]
        public void TestGetProgress()
        {
            int status = client.GetProgress();
            if (status >= 0 && status <= 100)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test()]
        public void TestGetStatus()
        {
            string status = client.GetStatus();
            Assert.AreEqual("", status);
        }

        public void Optimize(ServiceOptimizeClient client)
        {
            client.Optimize();
        }
    }
}
