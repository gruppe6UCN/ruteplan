using NUnit.Framework;
using System;
using Server;
using System.IO;
using System.Collections.Generic;
using System.Data;
using Model;

namespace TestServer
{
    [TestFixture()]
    public class TestDBCustomer
    {
        DBCustomer instance;
        String user;
        String pass;
        ulong id;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
           
            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            } catch { }

            DBConnection.Instance.Host = "localhost";
            DBConnection.Instance.DB = "TestArla";
            DBConnection.Instance.User = user;
            DBConnection.Instance.Pass = pass;

            DBConnection.Instance.Connect();

            instance = DBCustomer.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }


    }
}

