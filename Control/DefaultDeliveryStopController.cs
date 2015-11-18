using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Server.Database;
using FileHelpers;

namespace Control
{
    public class DefaultDeliveryStopController
    {
        public DBDefaultDeliveryStop DbDefaultDeliveryStop { get; private set; }
        public CustomerController CustomerCtr { get; private set; }
        private static DefaultDeliveryStopController instance;

        //Mapping Class for File Import.
        [IgnoreFirst()]
        [IgnoreLast()]
        [DelimitedRecord(",")]
        public class MappingDefaultRoute
        {
            public string Violation;
            public string Status;
            public string TransportationDate;
            public string OrderNo;
            public string E;
            public string OrderStatusName;
            public string OrderKind;
            public string ReasonCode;
            public string PickUp;
            public string Customer;
            public string SAPRoute;
            public string Route;
            public string Shipment;
            public string CUstomerNO;
            public string udf_sequencenumber;
            public string CustomerName;
            public string Zipcode;
            public string City;
            public string TWFrom;
            public string TWTill;
            public string ETA;
            public string PromisedTime;
            public string RCE;
            public string Distance;
            public string udf_deleteFromDB;
        }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DefaultDeliveryStopController() 
        {
            DbDefaultDeliveryStop = DBDefaultDeliveryStop.Instance;
            CustomerCtr = CustomerController.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DefaultDeliveryStopController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultDeliveryStopController();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets all the DefaultDeliveryStops from the database for the given route.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute which DefaultDeliveryStops are to be returned.</param>
        /// <returns>List of DefaultDeliveryStops for route.</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(DefaultRoute defaultRoute)
        {
            long defaultRouteID = defaultRoute.ID;
            List<DefaultDeliveryStop> stops = DbDefaultDeliveryStop.GetDefaultDeliveryStops(defaultRouteID);
            foreach (DefaultDeliveryStop stop in stops) {
                CustomerCtr.AddCustomers(stop);
            }
            return stops;
        }

        /// <summary>
        /// Gets all the DefaultDeliveryStops from a .csv file for the given route.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute which DefaultDeliveryStops are to be returned.</param>
        /// /// <param name="defaultRoute">Filepath of the .csv file to be imported.</param>
        /// <returns>List of DefaultDeliveryStops for route.</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(DefaultRoute defaultRoute, string path)
        {
            //Reads the file and maps it to mapping class.
            var engine = new FileHelperEngine<MappingDefaultRoute>();
            var records = engine.ReadFile(path);

            //Creates list of DefaultDeliveryStops.
            List<DefaultDeliveryStop> defaultDeliveryStops = new List<DefaultDeliveryStop>();

            //Converts mapping class to routes.
            foreach (var record in records)
            {
                DefaultRoute defaultRoute = new DefaultRoute(ParseID(record.Route), TrailerType.STOR, false);
                defaultDeliveryStops.Add(defaultRoute);
            }


            //Adds customers to each stop.
            foreach (DefaultDeliveryStop stop in defaultDeliveryStops)
            {
                Customer.Ctr.AddCustomers(stop);
            }


            //Returns List
            return defaultDeliveryStops;
        }
    }
}
