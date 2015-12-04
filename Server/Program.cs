using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using WCFService;
using Control;
using Database;
using GMap.NET;
using Model;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Starts Database
            WCFServer.Initialize();

            //Imports Data from Database
            string pathRoutes = "Config/RuterCSVTest.csv";
            string pathStops = "Config/stopsCSV.csv";
            string pathCustomers = "Config/kunderCSV.csv";
            //ImportController.Instance.ImportFromFile(pathRoutes, pathStops, pathCustomers);

            //Starts Server
            WCFServer.StartServer();

            List<DeliveryStop> allstop = new List<DeliveryStop>();
            //List<Route> routes = RouteController.Instance.Routes.ToList();
            //routes.ForEach(route =>
            //{
            //    allstop.AddRange(route.Stops);
            //});

            ConcurrentBag<String> herpderp = new ConcurrentBag<string>();

            List<GeoLoc> hah = DBGeoLoc.Instance.GetGeoLocs();
            int haha = 0;
            hah.ForEach(to =>
            {
                PointLatLng arlaFoodHobro = new PointLatLng(56.6372393, 9.7797216);

                var herp = MapController.MapProvider.GetRoute(arlaFoodHobro, to.Point, false, false, 15);
                if (herp == null)
                {
                    var derp = String.Format("Lat: {0}, Lng: {1}", to.Latitude,
                        to.Longitude);
                    Console.WriteLine(derp);
                    herpderp.Add(derp);
                }
                haha++;
                Console.WriteLine(haha + "/" + hah.Count);
            });
            
            File.WriteAllLines("herp-derp.txt", herpderp);

            Console.WriteLine("Jobs Done!!!");

            //Funtime?
            Console.Read();
            WCFServer.StopServer();
        }
    }
}
