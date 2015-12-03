using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFService;
using Control;
using Database;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Starts Database
            WCFServer.InitializeEmpty();

            //Imports Data from Database
            string pathRoutes = "Config/RuterCSVTest.csv";
            string pathStops = "Config/stopsCSV.csv";
            string pathCustomers = "Config/kunderCSV.csv";
            ImportController.Instance.ImportFromFile(pathRoutes, pathStops, pathCustomers);

            //Starts Server
            WCFServer.StartServer();

            //Funtime?
            Console.Read();
            WCFServer.StopServer();
        }
    }
}
