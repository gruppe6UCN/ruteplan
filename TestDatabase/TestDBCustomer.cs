using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using Database;
using Model;

namespace TestServer
{
    [TestFixture()]
    public class TestDBCustomer
    {
        DBCustomer instance;
        String user;
        String pass;

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

        [Test()]
        public void TestGetCustomers()
        {
            List<Customer> customers = instance.GetCustomers(555);
            foreach (Customer customer in customers)
            {
                Assert.Greater(customer.ID, 0);
                Assert.Greater(customer.City.Length, 0);
                Assert.Greater(customer.StreetName.Length, 0);
                Assert.Greater(customer.StreetNo.Length, 0);
                Assert.Greater(customer.Zipcode, 0);
                Assert.Less(customer.Zipcode, 9999);

                Assert.AreEqual(customer.ID, 21673);
                Assert.AreEqual(customer.City, "Brabrand");
                Assert.AreEqual(customer.StreetName, "Stenbækvej");
                Assert.AreEqual(customer.StreetNo, "1 A");
                Assert.AreEqual(customer.TimeOfDelivery, new TimeSpan(4,30,0));
                Assert.AreEqual(customer.Zipcode, 8220);
            }
        }
    }
}

