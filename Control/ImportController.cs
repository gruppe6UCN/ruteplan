using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using FileHelpers;

namespace Control
{
    public class ImportController
    {
        public RouteController RouteCtr { get; private set; }
        private static ImportController instance;

        //Mapping Class for File Import.
        [IgnoreFirst()]
        [IgnoreLast()]
        [DelimitedRecord(",")]
        public class MappingDefaultRoute
        {
            public string Vialolation;
            public string StatusViolation;
            public string Masterplan;
            public string TransportationDate;
            public string ShiftExportStatus;
            public string FirstActionState;
            public string SAP;
            public string Mobile;
            public string ReasonCode;
            public string Status;
            public string lastExportDate;
            public string Route;
            public string Shipment;
            public string Gate;
            public string Truck;
            public string Trailer;
            public string DriverNo;
            public string Driver;
            public string Vendor;
            public string Capacity;
            public string RCE;
            public string pct_Loaded;
            public string Start;
            public string DepartureFromDepot;
            public string udf_LateDeparture;
            public string Finish;
            public string V;
            public string S;
            public string LastDeliver;
            public string Delivers;
            public string DrivingTime;
            public string Duration;
            public string KM;
            public string Cost;
            public string udf_DeleteFromDB;
        }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private ImportController() 
        {
            RouteCtr = RouteController.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static ImportController Instance { 
                get { 
                    if (instance == null)
                        instance = new ImportController();
                    return instance;
                }
            }
    
        /// <summary>
        /// Imports all routes from database.
        /// </summary>
        public void ImportRoutes()
        {
            RouteCtr.ImportRoutes(DateTime.Now);
        }

        /// <summary>
        /// Imports all default routes from a .csv file.
        /// </summary>
        /// <param name="path">Path of .csv file to be imported.</param>
        public void ImportFromFile(string path)
        {
            //Reads the file and maps it to mapping class.
            var engine = new FileHelperEngine<MappingDefaultRoute>();
            var records = engine.ReadFile(path);

            //Creates list of default routes.
            List<DefaultRoute> defaultRoutes = new List<DefaultRoute>();

            //Converts mapping class to routes.
            foreach (var record in records)
            {
                DefaultRoute defaultRoute = new DefaultRoute(ParseID(record.Route), TrailerType.STOR, false);
                defaultRoutes.Add(defaultRoute);
            }

            //Creates routes from list.
            RouteCtr.ImportRoutes(defaultRoutes, DateTime.Now);
        }

        /// <summary>
        /// Parses the given route string to a long ID and returns it.
        /// </summary>
        /// <param name="id">String to be parsed.</param>
        /// <returns>Long value of id.</returns>
        private long ParseID(string id)
        {
            string idString = id.Substring(3);
            return long.Parse(idString);
        }

    }
}
