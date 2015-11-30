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
    class TestLogController
    {
        LogController lc;

        [SetUp()]
        public void SetUp()
        {
            this.lc = LogController.Instance;
        }

        [Test()]
        public void TestXXXXX()
        {

        }
    }
}
