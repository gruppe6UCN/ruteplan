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
