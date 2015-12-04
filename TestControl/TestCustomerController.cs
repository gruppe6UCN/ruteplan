using NUnit.Framework;
using Control;
using Database;
using Model;

namespace ControlTest
{
    [TestFixture()]
    class TestCustomerController
    {
        CustomerController cc;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            Server.DBClient.Initialize();
        }

        [SetUp()]
        public void SetUp()
        {
            this.cc = CustomerController.Instance; 
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
        }

        [Test()]
        public void TestAddCustomer()
        {
            DefaultDeliveryStop stop = new DefaultDeliveryStop(651, 20);
            cc.AddCustomers(stop);
            Assert.IsNotEmpty(stop.Customers);
        }

        public void TestAddCustomerFromFile()
        {
            string pathCustomers = "Config/kunderCSV.csv";
            cc.GetCustomersFromFile(pathCustomers);
            var stop = new DefaultDeliveryStopController.TmpDefaultDeliveryStop(10, 20, 30, 40, 50, "10", "10", 20.1, new GeoLoc(1, 2, 3));
            cc.AddCustomersFromFile(stop);
            Assert.IsNotEmpty(stop.Customers);           
        }

        public void TestGetCustomersFromFile()
        {
            string pathCustomers = "Config/kunderCSV.csv";
            cc.GetCustomersFromFile(pathCustomers);
            Assert.IsNotEmpty(cc.records);
        }

    }
}
