using NUnit.Framework;
using System;
using Server;

namespace TestServer
{
    [TestFixture()]
    public class Test
    {
        DBConnection instance;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            instance = DBConnection.GetInstance();
        }

        [Test()]
        public void TestGetInstance()
        {
            Assert.AreEqual(instance, DBConnection.GetInstance());
        }

        [Test()]
        public void TestConnect()
        {
            instance.Host = "localhost";
            instance.DB = "Arla";
            instance.User = "unknown";
            instance.Pass = "Unknown";

            try
            {
                instance.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

