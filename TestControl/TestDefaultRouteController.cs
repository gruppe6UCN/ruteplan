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
    class TestDefaultRouteController
    {
        DefaultRouteController drc;

        [SetUp()]
        public void SetUp()
        {
            this.drc = DefaultRouteController.Instance;
        }

        [Test()]
        public void TestXXXXXX()
        {

        }
    }
}
