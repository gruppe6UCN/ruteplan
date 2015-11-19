﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
        String user;
        String pass;

        [TestFixtureSetUp()]
        public void ClassSetUp()
        {
            try {
                user = File.ReadAllText(@"C:\Users\The Baron\Dropbox\3. Projekt\Scripts\Config\user.txt");
                pass = File.ReadAllText(@"C:\Users\The Baron\Dropbox\3. Projekt\Scripts\Config\pass.txt");
            }
            catch { }

            DBConnection.Instance.Host = "localhost";
            DBConnection.Instance.DB = "TestArla";
            DBConnection.Instance.User = user;
            DBConnection.Instance.Pass = pass;
            DBConnection.Instance.Connect();

            ic = ImportController.Instance;
            rc = RouteController.Instance;
        }

        [TestFixtureTearDown()]
        public void ClassTearDown()
        {
            DBConnection.Instance.Disconnect();
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
            string pathRoutes = @"C:\Users\The Baron\Dropbox\3. Projekt\Arla Food\RuterCSVTest.csv";
            string pathStops = @"C:\Users\The Baron\Dropbox\3. Projekt\Arla Food\stopsCSV.csv";
            string pathCustomers = @"C:\Users\The Baron\Dropbox\3. Projekt\Arla Food\kunderCSV.csv";
            ic.ImportFromFile(pathRoutes, pathStops, pathCustomers);
            Assert.IsNotEmpty(rc.Routes);
        }
    }
}
