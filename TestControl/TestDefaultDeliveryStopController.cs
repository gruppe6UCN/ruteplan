using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Control;
using Server;


namespace ControlTest
{
    [TestFixture()]
    class TestDefaultDeliveryStopController
    {
        DefaultDeliveryStopController ddsc;
             
        [SetUp()]
        public void SetUp()
        {
            this.ddsc = DefaultDeliveryStopController.Instance;
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
