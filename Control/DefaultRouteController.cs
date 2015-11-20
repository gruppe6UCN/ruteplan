using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using System.Collections.Concurrent;
using Model;
using FileHelpers;

namespace Control
{
    public class DefaultRouteController
    {
        public DBDefaultRoute DbDefaultRoute { get; private set; }
        private static DefaultRouteController instance;

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
            public string UdfLateDeparture;
            public string Finish;
            public string V;
            public string S;
            public string LastDeliver;
            public string Delivers;
            public string DrivingTime;
            public string Duration;
            public string KM;
            public string Cost;
            public string UdfDeleteFromDB;
        }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DefaultRouteController() 
        {
            DbDefaultRoute = DBDefaultRoute.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DefaultRouteController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DefaultRouteController();
                return instance;
            }
        }

        /// <summary>
        /// Gets a list of all default routes from the database.
        /// </summary>
        /// <returns>List of default routes.</returns>
        public List<DefaultRoute> GetDefaultRoutes()
        {
            //Gets a list of all defaultRoutes.
            List<DefaultRoute> list = DbDefaultRoute.DefaultRoutes();

            //Returns the list.
            return list;
        }

        /// <summary>
        /// Gets a list of all default routes from a .csv file.
        /// </summary>
        /// <param name="path">Path of .csv file to be imported.</param>
        public List<DefaultRoute> GetDefaultRoutes(string path)
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

            //Returns DefaultRoutes.
            return defaultRoutes;
        }

        /// <summary>
        /// Stores given default route to the database.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute to be stored.</param>
        public void store(DefaultRoute defaultRoute)
        {
            DbDefaultRoute.store(defaultRoute);
        }

        /// <summary>
        /// Parses the given route string to a long ID and returns it.
        /// </summary>
        /// <param name="id">String to be parsed.</param>
        /// <returns>Long value of id.</returns>
        public static long ParseID(string id)
        {
            try
            {
                string idString = id.Substring(3);
                return long.Parse(idString);
            }
            catch { throw; }
        }

    }
}
