using NUnit.Framework;
using System;
using Server;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using Model;

namespace TestServer
{
    [TestFixture()]
    public class Test
    {
        DBConnection instance;
        String user;
        String pass;
        ulong id;

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
        }

        [Test()]
        public void Test_01_ForUsernameAndPasswordFiles()
        {
            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test()]
        public void Test_02_GetInstance()
        {
            Assert.AreEqual(instance, DBConnection.Instance);
            Assert.NotNull(instance);
        }

        [Test()]
        public void Test_03_Connect()
        {
            if (!instance.IsConnected)
                instance.Connect();
            Assert.IsTrue(instance.IsConnected);
        }

        [Test()]
        public void Test_04_Disconnect()
        {
            instance.Disconnect();
            Assert.IsFalse(instance.IsConnected);

            if (!instance.IsConnected)
                instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'Host'")]
        public void Test_05_Connect_ExceptionForHost()
        {
            instance.Host = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'DB'")]
        public void Test_06_Connect_ExceptionForDB()
        {
            instance.DB = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'User'")]
        public void Test_07_Connect_ExceptionForUser()
        {
            instance.User = null;

            instance.Connect();
        }

        [Test()]
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage="You need to initialize the accessor 'Pass'")]
        public void Test_08_Connect_ExceptionForP()
        {
            instance.Pass = null;

            instance.Connect();
        }

        [Test()]
        public void Test_09_SendInsertSQL()
        {
            id = instance.SendInsertSQL("INSERT into DefaultRoute (trailer_type, extra_route) values('STOR', 0)");
            Assert.Greater(id, 0);
        }

        [Test()]
        public void Test_10_SendUpdateSQL()
        {
            int r = instance.SendUpdateSQL(String.
                Format("UPDATE DefaultRoute SET extra_route=1 WHERE id={0}", id));
            Assert.Greater(r, 0);
        }

        [Test()]
        public void Test_11_SendSQL()
        {
            List<DefaultRoute> r = instance.SendSQL<DefaultRoute>("SELECT * FROM DefaultRoute", DelegateMethod);
            Assert.IsTrue(r[r.Count - 1].isExtraRoute());
            Assert.AreEqual(r[r.Count - 1].id, id);
        }

        public List<DefaultRoute> DelegateMethod(IDataReader data) {
            List<DefaultRoute> r = new List<DefaultRoute>();
            while (data.Read())
            {
                IDataRecord row = ((IDataRecord)data);
                r.Add(new DefaultRoute(
                    row.GetInt64(0),
                    row.GetString(1),
                    row.GetBoolean(2))
                );
            }
            return r;
        }
    }
}

