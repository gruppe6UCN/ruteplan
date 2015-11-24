using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Model;
using FileHelpers;

namespace Control
{
    public class CustomerController
    {
        public DBCustomer DbCustomer { get; private set; }
        public FileHelperEngine<MappingCustomer> engine { get; private set; }
        public List<MappingCustomer> records { get; private set; }
        private static CustomerController instance;

        //Mapping Class for File Import.
        [IgnoreFirst(2)]
        [IgnoreLast()]
        [DelimitedRecord(";")]
        public class MappingCustomer 
        {
            public string AAAANAAALL;
            public string Active;
            public string UdfDepotld;
            public long CustomerNo;
            public string Name;
            public string StreetName;
            public string DoorNumber;
            public string AreaDescription;
            public int    ZipCode;
            public string City;
            public double X;
            public double Y;
        }


        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private CustomerController()
        {
            DbCustomer = DBCustomer.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static CustomerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerController();
                }
                return instance;
            }
        }

        /// <summary>
        /// Adds customers to the given default delivery stop.
        /// </summary>
        /// <param name="defaultStop"></param>
        public void AddCustomers(DefaultDeliveryStop defaultStop) 
        {
            defaultStop.Customers = DbCustomer.GetCustomers(defaultStop.ID);
        }

        /// <summary>
        /// Adds customers to the given default delivery stop. Customers must be added using the method
        /// GetCustomersFromFile beforehand.
        /// </summary>
        /// <param name="defaultStop">Default stop to add customers to.</param>
        /// <param name="dic">Dictionary containing time for customer.</param>
        public void AddCustomersFromFile(DefaultDeliveryStopController.TmpDefaultDeliveryStop defaultStop)
        {           
            //Creates list of Customers.
            List<Customer> customers = new List<Customer>();

            //Converts mapping class to stops.
            foreach (var record in records)
            {
                //Validates customer.
                if (record.CustomerNo == defaultStop.CustomerID)
                {
                    DateTime date = ParseToDateTime(defaultStop.PromisedTime, defaultStop.TransportationDate);
                    TimeSpan time = ParseToTimeSpan(date);
                    Customer customer = new Customer(record.CustomerNo, record.StreetName,
                        record.DoorNumber, record.ZipCode, record.City, time);
                    customers.Add(customer);
                }
            }

            //Adds customers to stop.
            defaultStop.Customers = customers;
        }

        /// <summary>
        /// Creates a list of customers from the given .csv file.
        /// </summary>
        /// <param name="path">File path of .csv file to be imported.</param>
        public void GetCustomersFromFile(string path)
        {
            //Reads the file and maps it to mapping class.
            engine = new FileHelperEngine<MappingCustomer>();
            records = engine.ReadFileAsList(path);
        }

        /// <summary>
        /// Converts the given date and time strings from the mapping class to a DateTime.
        /// </summary>
        /// <param name="time">String to be converted. Format "HH:MM"</param>
        /// <param name="date">String to be converted. Format "MM:DD"</param>
        /// <returns>DateTime object for date.</returns>
        private DateTime ParseToDateTime(string time, string date)
        {
            int year = DateTime.Now.Year;
            int month = int.Parse(date.Substring(0, 2));
            int day = int.Parse(date.Substring(3));
            int hour = int.Parse(time.Substring(0, 2));
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
