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
        [IgnoreFirst()]
        [IgnoreLast()]
        [DelimitedRecord(",")]
        public class MappingCustomer 
        {
            public string Active;
            public string UdfDepotld;
            public long CustomerNo;
            public string Name;
            public string StreetName;
            public string DoorNumber;
            public string AreaDescription;
            public int    ZipCode;
            public string City;
            public string X;
            public string Y;
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
        public void AddCustomersFromFile(DefaultDeliveryStop defaultStop, Dictionary<DateTime, DefaultDeliveryStopController.MappingDefaultDeliveryStop> dic)
        {

            //Creates list of Customers.
            List<Customer> customers = new List<Customer>();
            long id = 1;

            //Converts mapping class to stops.
            foreach (var record in records)
            {

                Customer customer = new Customer(record.CustomerNo, record.StreetName, record.DoorNumber, record.ZipCode, record.City, )


                DefaultDeliveryStop defaultStop = new DefaultDeliveryStop(id, record);



                customers.Add(defaultStop);
                id++;


                DefaultRoute defaultRoute = new DefaultRoute(ParseID(record.Route), TrailerType.STOR, false);
                customers.Add(defaultRoute);
            }

            
            
            defaultStop.Customers = DbCustomer.GetCustomers(defaultStop.ID);
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

    }
}
