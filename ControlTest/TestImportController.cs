using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Control;
using Server;
using Model;

namespace ControlTest
{
    [TestFixture()]
    public class TestImportController
    {
        ImportController ic;
        RouteController rc;

        [SetUp()]
        public void SetUp()
        {
            ic = ImportController.Instance;
            rc = RouteController.Instance;
        }

        [Test()]
        public void TestImportRoutes()
        {
            ic.ImportRoutes();
            Assert.IsNotEmpty(rc.Routes);
        }

        [Test()]
        public void TestImportRoutesFromFile()
        {
            ic.ImportFromFile("C:\\Users\\The Baron\\Dropbox\\3. Projekt\\Arla Food\\EventyrIBarbiesPrincesseSLot.csv");
            Assert.IsNotEmpty(rc.Routes);
        }
    }
}
