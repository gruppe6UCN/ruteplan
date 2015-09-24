using System;
using NUnit.Framework;
using Model;

namespace TestModel
{
     [TestFixture()]

    public class TestCustomer
    {
         Customer c;

         [SetUp()]
         public void SetUp()
         {
             c = new Customer(10, "Rejegade", "5", 9080,"Aalborg", 12);
         }

         [Test()]
         //Test for Customer ID
         public void TestID()
         {
             Assert.AreEqual(c.ID, 10);
         }
         //Test for Customer StreetName
         public void TestStreetName()
         {
             Assert.AreEqual(c.StreetName, "Rejegade");
         }
         //Test for Customer StreetNumber
         public void TestStreetNo()
         {
             Assert.AreEqual(c.StreetNo, "5");
         }
         //Test for Customer Zipcode
         public void TestZipcode()
         {
             Assert.AreEqual(c.Zipcode, 9080);
         }
         //Test for Customer City
         public void TestCity()
         {
             Assert.AreEqual(c.City, "Aalborg");
         }
         //Test for Time of Delivery
         public void TestTimeOfDelivery()
         {
             Assert.AreEqual(c.TimeOfDelivery, 12);
         }
        

    }
}
