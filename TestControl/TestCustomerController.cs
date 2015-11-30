using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
