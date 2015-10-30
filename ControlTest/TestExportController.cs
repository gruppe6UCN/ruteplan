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
    class TestExportController
    {
        ExportController ec;

        [SetUp()]
        public void SetUp()
        {
            this.ec = new ExportController();
        }

        [Test()]
        public void TestXXXX()
        {

        }
    }
}
