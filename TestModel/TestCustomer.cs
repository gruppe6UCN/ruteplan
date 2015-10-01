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
             this.c = new Customer(10, "Rejegade", "5", 9080,"Aalborg", new DateTime(12));
         }

         //Test for Customer ID
         [Test()]
         public void TestID()
         {
             Assert.AreEqual(c.ID, 10);
         }
         //Test for Customer StreetName
         [Test()]
         public void TestStreetName()
         {
             Assert.AreEqual(c.StreetName, "Rejegade");
         }
         //Test for Customer StreetNumber
         [Test()]
         public void TestStreetNo()
         {
             Assert.AreEqual(c.StreetNo, "5");
         }
         //Test for Customer Zipcode
         [Test()]
         public void TestZipcode()
         {
             Assert.AreEqual(c.Zipcode, 9080);
         }
         //Test for Customer City
         [Test()]
         public void TestCity()
         {
             Assert.AreEqual(c.City, "Aalborg");
         }
         
         //Test for Time of Delivery
         [Test()]
         public void TestTimeOfDelivery()
         {
             Assert.AreEqual(c.TimeOfDelivery, 12);
         }
        

    }
}
