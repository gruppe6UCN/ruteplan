using NUnit.Framework;
using System;
using Server;
using System.IO;

namespace TestServer
{
    [TestFixture()]
    public class Test
    {
        DBConnection instance;
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            instance = DBConnection.Instance;
            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            } catch { }
        }

        [SetUp()]
        public void Setup()
        {
            instance.Host = "localhost";
            instance.DB = "TestArla";
            instance.User = user;
            instance.Pass = pass;

            if (!instance.IsConnected)
                instance.Connect();
        }

        [Test()]
        public void TestForUsernameAndPasswordFiles()
        {
            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }

        [Test()]
        public void TestGetInstance()
        {
            Assert.AreEqual(instance, DBConnection.Instance);
            Assert.NotNull(instance);
        }

        [Test()]
        public void TestConnect()
        {
            if (!instance.IsConnected)
                instance.Connect();
            Assert.IsTrue(instance.IsConnected);
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'Host'")]
        public void TestConnect_ExceptionForHost()
        {
            instance.Host = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'DB'")]
        public void TestConnect_ExceptionForDB()
        {
            instance.DB = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'User'")]
        public void TestConnect_ExceptionForUser()
        {
            instance.User = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'Pass'")]
        public void TestConnect_ExceptionForP()
        {
            instance.Pass = null;

            instance.Connect();
        }

        [Test()]
        public void TestDisconnect()
        {
            instance.Disconnect();
            Assert.IsFalse(instance.IsConnected);
        }
    }
}

