using System;
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
