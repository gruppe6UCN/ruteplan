using NUnit.Framework;
using Control;

namespace ControlTest
{
    [TestFixture()]
    class TestCustomerController
    {
        CustomerController cc; 

        [SetUp()]
        public void SetUp()
        {
            this.cc = CustomerController.Instance; 
        }

        [Test()]
        public void TestXXXXX()
        {

        }

    }
}
