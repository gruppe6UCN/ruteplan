using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Model;
using FileHelpers;

namespace Control
{
    public class DefaultDeliveryStopController
    {
        public DBDefaultDeliveryStop DbDefaultDeliveryStop { get; private set; }
        public DBGeoLoc DbGeoLoc { get; private set; }
        public CustomerController CustomerCtr { get; private set; }
        public MapController mapCtr { get; private set; }
        public List<TmpDefaultDeliveryStop> TmpDefaultStops { get; private set; }
        public Dictionary<long, TimeSpan> dic { get; private set; }
        private GeoLoc geoLoc;
        private static DefaultDeliveryStopController instance;

        public class TmpDefaultDeliveryStop
        {
            public long ID { get; private set; }
            public long CustomerID { get; private set; }
            public long GeoLocID { get; private set; }
            public long RouteID { get; private set; }
            public List<Customer> Customers { get; set; }
            public int? SequenceNbr { get; private set; }
            public string PromisedTime { get; private set; }
            public string TransportationDate { get; private set; }
            public double RCE { get; private set; }
            public GeoLoc GeoLocation { get; private set; }

            public TmpDefaultDeliveryStop(long ID, long GeoLocID, long RouteID, 
                long CustomerID, int? SequenceNbr, string PromisedTime, 
                string TransportationDate, double RCE, GeoLoc GeoLocation)
            {
                this.ID = ID;
                this.GeoLocID = GeoLocID;
                this.RouteID = RouteID;
                this.CustomerID = CustomerID;
                this.SequenceNbr = SequenceNbr;
                this.PromisedTime = PromisedTime;
                this.TransportationDate = TransportationDate;
                this.RCE = RCE;
                this.GeoLocation = GeoLocation;
            }
        }

        //Mapping Class for File Import.
        [IgnoreFirst()]
        [IgnoreLast()]
        [DelimitedRecord(";")]
        public class MappingDefaultDeliveryStop
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
            public long CustomerNO;
            public string UdfSequencenumber;
            public string CustomerName;
            public string Zipcode;
            public string City;
            public string TWFrom;
            public string TWTill;
            public string ETA;
            public string PromisedTime;
            public double RCE;
            public string Distance;
            public string UdfDeleteFromDB;
        }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DefaultDeliveryStopController() 
        {
            DbDefaultDeliveryStop = DBDefaultDeliveryStop.Instance;
            CustomerCtr = CustomerController.Instance;
            DbGeoLoc = DBGeoLoc.Instance;
            mapCtr = MapController.Instance;
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
            Parallel.ForEach(stops, stop =>
            {
                CustomerCtr.AddCustomers(stop);
                mapCtr.AddGeoLog(stop);
            });
            return stops;
        }

        /// <summary>
        /// Gets a list of all default delivery stops imported from a .csv file for the given route.
        /// List must be created first by using the method ImportDefaultDeliveryStopFromFile.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute which DefaultDeliveryStops are to be returned.</param>
        /// <returns>List of DefaultDeliveryStops for route.</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStopsFromFile(DefaultRoute defaultRoute)
        {
            List<DefaultDeliveryStop> stops = new List<DefaultDeliveryStop>();

            foreach (TmpDefaultDeliveryStop stop in TmpDefaultStops)
            {
                if (stop.RouteID == defaultRoute.ID)
                {
                    DefaultDeliveryStop defaultStop = new DefaultDeliveryStop(stop.ID, stop.GeoLocID, stop.SequenceNbr);
                    defaultStop.Customers = stop.Customers;
                    defaultStop.GeoLoc = stop.GeoLocation;
                    stops.Add(defaultStop);
                }
            }

            DbDefaultDeliveryStop.StoreDefaultDeliveryStopHAX(stops, defaultRoute.ID);
            foreach (DefaultDeliveryStop stop in stops)
            {
                //Stores custoemrs to database...
                DBCustomer.Instance.StoreCustomer(stop.Customers, stop.ID);
            }

            return stops;
        }

        /// <summary>
        /// Gets all the DefaultDeliveryStops from a .csv file for the given route.
        /// </summary>
        /// <param name="pathStops">Filepath of the .csv file for stops to be imported.</param>
        /// <param name="pathCustomers">Filepath of the .csv file for customers.</param>
        public void ImportDefaultDeliveryStopsFromFile(string pathStops, string pathCustomers)
        {
            //Reads the file and maps it to mapping class.
            FileHelperEngine<MappingDefaultDeliveryStop> engine = new FileHelperEngine<MappingDefaultDeliveryStop>();
            List<MappingDefaultDeliveryStop> records = engine.ReadFileAsList(pathStops);
            CustomerCtr.GetCustomersFromFile(pathCustomers);

            //Creates various lists for use in code.
            TmpDefaultStops = new List<TmpDefaultDeliveryStop>();
            Dictionary<double, GeoLoc> geoLocDic = new Dictionary<double, GeoLoc>();
            long geoId = 1;
            long id = 1;

            //Creates dictionary for geoLocs.
            foreach (CustomerController.MappingCustomer customer in CustomerCtr.records)
            {
                double xx = customer.X;
                double yy = customer.Y;

                if (geoLocDic.ContainsKey(yy)) {
                    continue;
                }
                    
                GeoLoc geoLoc = new GeoLoc(geoId, yy, xx);
                geoLocDic.Add(yy, geoLoc);
                geoId++;
            }

            //Stores geoLocs to database.
            List<GeoLoc> geoLocList = new List<GeoLoc>();
            foreach(var value in geoLocDic.Values)
            {
                geoLocList.Add(value);
            }
            DbGeoLoc.StoreGeoLoc(geoLocList);

            //Converts mapping class to stops.
            foreach (MappingDefaultDeliveryStop record in records)
            {               
                //Does shit with geolocs...
                foreach (CustomerController.MappingCustomer customer in CustomerCtr.records)
                {
                    long idCustomerStop = record.CustomerNO;
                    long idCustomer = customer.CustomerNo;

                    if (idCustomerStop == idCustomer)
                    {
                        geoLoc = geoLocDic[customer.Y];
                        break;
                    }
                }

                try
                {
                    TmpDefaultDeliveryStop defaultStop = new TmpDefaultDeliveryStop(
                        id,
                        geoLoc.ID,
                        DefaultRouteController.ParseID(record.SAPRoute),
                        record.CustomerNO, ParseToInt(record.UdfSequencenumber),
                        record.PromisedTime,
                        record.TransportationDate,
                        record.RCE,
                        geoLoc);

                    TmpDefaultStops.Add(defaultStop);
                    id++;
                }
                catch (FormatException e) { /* Console.WriteLine("Invalid SAPRoute ID {0}", e); */ }
            }

            foreach (TmpDefaultDeliveryStop stop in TmpDefaultStops)
            {
                CustomerCtr.AddCustomersFromFile(stop);
            }
        }

        /// <summary>
        /// Converts the given string to a nullable int?.
        /// </summary>
        /// <param name="str">String to be parsed.</param>
        /// <returns>Int of string. Null if "".</returns>
        private int? ParseToInt(string str)
        {
            int? value;

            if (string.Equals(str, ""))
            {
                value = null;
            }
            else
            {
                value = int.Parse(str);
            }

            return value;
        }
    }
}
