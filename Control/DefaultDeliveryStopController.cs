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
        /// <param name="pathStops">Filepath of the .csv file for stops to be imported.</param>
        /// <param name="pathCustomers">Filepath of the .csv file for customers.</param>
        /// <returns>List of DefaultDeliveryStops for route.</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(DefaultRoute defaultRoute, string pathStops, string pathCustomers)
        {
            //Reads the file and maps it to mapping class.
            FileHelperEngine<MappingDefaultDeliveryStop> engine = new FileHelperEngine<MappingDefaultDeliveryStop>();
            List<MappingDefaultDeliveryStop> records = engine.ReadFileAsList(pathStops);
            CustomerCtr.GetCustomersFromFile(pathCustomers);

            //Creates various lists for use in code.
            List<DefaultDeliveryStop> defaultDeliveryStops = new List<DefaultDeliveryStop>();
            Dictionary<DateTime, MappingDefaultDeliveryStop> dic = new Dictionary<DateTime, MappingDefaultDeliveryStop>();
            long id = 1;

            //Creates dictionary for time in use for customer.
            foreach (MappingDefaultDeliveryStop record in records)
            {
                dic.Add(ParseToDateTime(record.PromisedTime, record.TransportationDate), record);
            }

            //Converts mapping class to stops.
            foreach (MappingDefaultDeliveryStop record in records)
            {
                



                
                
                DefaultDeliveryStop defaultStop = new DefaultDeliveryStop(id, );

                

                



                defaultDeliveryStops.Add(defaultStop);
                id++;
                
                

            }


            //Adds customers to each stop.
            foreach (DefaultDeliveryStop stop in defaultDeliveryStops)
            {
                CustomerCtr.AddCustomers(stop, dic);
            }

            //Returns List
            return defaultDeliveryStops;
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

    }
}
