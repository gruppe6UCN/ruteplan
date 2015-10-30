using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
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
            List<TransportUnit> listTP = instance.getTransportUnits(listID);
            Assert.IsNotEmpty(listTP);

            //Checks to see if element exists.
            TransportUnit unit = null;
            try
            {
                unit = listTP.First(u =>
                    {
                        return (u.ID == 1); //NOTE: No transport units currently exists in database. Test will not work.
                    });
            }
            catch (InvalidOperationException e) { }
            Assert.IsNotNull(unit);
        }
    }
}
