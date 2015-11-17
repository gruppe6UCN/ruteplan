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
    class TestTransportUnitController
    {
        TransportUnitController tuc;

        [SetUp()]
        public void Setup()
        {
            this.tuc = TransportUnitController.Instance;
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
