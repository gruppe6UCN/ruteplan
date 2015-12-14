using System;
using System.Collections.Concurrent;
using WCFService;
using GMap.NET;
using Control;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Starts Database
            DBClient.Initialize();

            //Imports Data from Database
            string pathRoutes = "Config/RuterCSVTest.csv";
            string pathStops = "Config/stopsCSV.csv";
            string pathCustomers = "Config/kunderCSV.csv";
            //TODO: Import from file and not Database.
            ImportController.Instance.ImportFromFile(pathRoutes, pathStops, pathCustomers);

            Console.WriteLine("Server Started");

            //Starts Server
            WCFServer.StartServer();

            //Press any key to close server
            Console.Read(); 

            //Stops Server
            WCFServer.StopServer();
        }
    }
}
