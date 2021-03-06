﻿using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Database;
using Model;

namespace TestServer
{
    [TestFixture()]
    public class TestDBConnection
    {
        DBConnection instance;
        String user;
        String pass;
        long id;

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

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            instance.Disconnect();
        }

        //[Test()]
        //public void TestInOrder()
        //{
        //    Test_01_ForUsernameAndPasswordFiles();
        //    Test_02_GetInstance();
        //    Test_03_Connect();
        //    Test_04_Disconnect();
        //    Test_05_Connect_ExceptionForHost();
        //    Test_06_Connect_ExceptionForDB();
        //    Test_07_Connect_ExceptionForUser();
        //    Test_08_Connect_ExceptionForP();
        //    Test_09_SendInsertSQL();
        //    Test_10_SendUpdateSQL();
        //    Test_11_SendSQL();
        //}

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
        [ExpectedException(typeof(NullReferenceException), ExpectedMessage = "You need to initialize the accessor 'Host'")]
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
            id = instance.SendInsertSQL("INSERT into DefaultRoute (trailer_type, extra_route) values(51.0, 0)");
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
            Assert.IsTrue(r[r.Count - 1].ExtraRoute);
            Assert.AreEqual(r[r.Count - 1].ID, id);
        }

        private List<DefaultRoute> DelegateMethod(IDataReader dataSet) {
            List<DefaultRoute> r = new List<DefaultRoute>();
            while (dataSet.Read())
            {
                IDataRecord row = ((IDataRecord)dataSet);
                r.Add(new DefaultRoute(
                    row.GetInt64(0),
                    row.GetDouble(1),
                    row.GetBoolean(2))
                );
            }
            return r;
        }
    }
}

