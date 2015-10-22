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
    class TestLogController
    {
        LogController lc;

        [SetUp()]
        public void SetUp()
        {
            this.lc = new LogController();
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
