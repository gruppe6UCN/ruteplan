using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Model;

namespace TestServer
{
    [TestFixture()]
    class TestDBTransportUnit
    {
        DBTransportUnit instance;
        DBCustomer instance_customer;
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {

            try
            {
                user = File.ReadAllText("Config/user.txt");
                pass = File.ReadAllText("Config/pass.txt");
            }
            catch { }

            DBConnection.Instance.Host = "localhost";
            DBConnection.Instance.DB = "TestArla";
            DBConnection.Instance.User = user;
            DBConnection.Instance.Pass = pass;

            DBConnection.Instance.Connect();

            instance = DBTransportUnit.Instance;
            instance_customer = DBCustomer.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestGetTransportUnits()
        {
            //Creates a list of customer ID's.
            List<Customer> customers = instance_customer.GetCustomers(555);
            List<long> listID = new List<long>();
            foreach (Customer c in customers)
            {
                listID.Add(c.ID);
            }

            //Gets a list of transport units.
            List<TransportUnit> listTU = instance.GetTransportUnits(listID);
            Assert.IsNotEmpty(listTU);

            //Checks to see if element exists.
            Assert.IsNotEmpty(listTU);
            listTU.ForEach(unit =>
            {
                Assert.IsTrue(listID.Contains(unit.CustomerID));
            });
        }
    }
}
