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
    class TestDeliveryStopController
    {
        DeliveryStopController dsc;

        [SetUp()]
        public void SetUp()
        {
            this.dsc = DeliveryStopController.Instance;
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
