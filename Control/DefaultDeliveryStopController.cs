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

            public TmpDefaultDeliveryStop(long ID, long GeoLocID, long RouteID, long CustomerID)
            {
                this.ID = ID;
                this.GeoLocID = GeoLocID;
                this.RouteID = RouteID;
                this.CustomerID = CustomerID;
            }
        }

        //Mapping Class for File Import.
        [IgnoreFirst()]
        [IgnoreLast()]
        [DelimitedRecord(",")]
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
        /// Gets a list of all default delivery stops imported from a .csv file for the given route.
        /// List must be created first by using the method GetDefaultDeliveryStopFromFile.
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
                    DefaultDeliveryStop defaultStop = new DefaultDeliveryStop(stop.ID, stop.GeoLocID);
                    defaultStop.Customers = stop.Customers;
                }
            }

            return stops;
        }

        /// <summary>
        /// Gets all the DefaultDeliveryStops from a .csv file for the given route.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute which DefaultDeliveryStops are to be returned.</param>
        /// <param name="pathStops">Filepath of the .csv file for stops to be imported.</param>
        /// <param name="pathCustomers">Filepath of the .csv file for customers.</param>
        public void ImportDefaultDeliveryStopsFromFile(DefaultRoute defaultRoute, string pathStops, string pathCustomers)
        {
            //Reads the file and maps it to mapping class.
            FileHelperEngine<MappingDefaultDeliveryStop> engine = new FileHelperEngine<MappingDefaultDeliveryStop>();
            List<MappingDefaultDeliveryStop> records = engine.ReadFileAsList(pathStops);
            CustomerCtr.GetCustomersFromFile(pathCustomers);

            //Creates various lists for use in code.
            dic = new Dictionary<long, TimeSpan>();
            Dictionary<double, double> geoLocDic = new Dictionary<double,double>();
            Dictionary<double ,GeoLoc> geoLocs = new Dictionary<double, GeoLoc>();
            long geoId = 1;
            long id = 1;

            //Creates dictionary for time in use for customer.
            foreach (MappingDefaultDeliveryStop record in records)
            {
                DateTime date = ParseToDateTime(record.PromisedTime, record.TransportationDate);
                TimeSpan time = ParseToTimeSpan(date);
                dic.Add(record.CustomerNO, time);
            }

            //Creates dictionary for geoLocs.
            foreach (CustomerController.MappingCustomer customer in CustomerCtr.records)
            {
                double xx = customer.X;
                double yy = customer.Y;

                if (geoLocDic.ContainsKey(yy)) {
                    continue;
                }
                    
                GeoLoc geoLoc = new GeoLoc(geoId, yy, xx);
                geoLocDic.Add(yy, xx);
                geoLocs.Add(yy, geoLoc);
                geoId++;
            }

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
                        geoLoc = geoLocs[customer.Y];
                        break;
                    }
                }

                TmpDefaultDeliveryStop defaultStop = new TmpDefaultDeliveryStop(id, geoLoc.ID, DefaultRouteController.ParseID(record.SAPRoute), record.CustomerNO);
                TmpDefaultStops.Add(defaultStop);
                id++;
            }

            foreach (TmpDefaultDeliveryStop stop in TmpDefaultStops)
            {
                CustomerCtr.AddCustomersFromFile(stop, dic);
            }
        }

        /// <summary>
        /// Converts the given date and time strings from the mapping class to a DateTime.
        /// </summary>
        /// <param name="date">String to be converted. Format "MM:DD"</param>
        /// <param name="time">String to be converted. Format "HH:MM"</param>
        /// <returns>DateTime object for date.</returns>
        private DateTime ParseToDateTime(string date, string time)
        {
            int year = DateTime.Now.Year;
            int month = int.Parse(date.Substring(0, 1));
            int day = int.Parse(date.Substring(3));
            int hour = int.Parse(time.Substring(0, 1));
            int minute = int.Parse(time.Substring(3));

            DateTime dateTime = new DateTime(year, month, day, hour, minute, 0);
            return dateTime;
        }

        /// <summary>
        /// Converts given date time to a timespan.
        /// </summary>
        /// <param name="?">DateTime to be converted.</param>
        /// <returns>TimeSpan for datetime.</returns>
        private TimeSpan ParseToTimeSpan(DateTime date)
        {
            TimeSpan time = date.TimeOfDay;
            return time;
        }

    }
}
