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
    class TestRouteController
    {
        RouteController rc;

        [SetUp()]
        public void SetUp()
        {
            this.rc = RouteController.Instance;
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
