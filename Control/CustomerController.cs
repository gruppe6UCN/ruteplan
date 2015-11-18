using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Model;

namespace Control
{
    public class CustomerController
    {
        public DBCustomer DbCustomer { get; private set; }
        private static CustomerController instance;

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
        /// Adds customers to the given default delivery stop. Customers is gained from the .csv file.
        /// </summary>
        /// <param name="defaultStop">Default stop to add customers to.</param>
        /// <param name="path">File path of .csv file.</param>
        public void AddCustomers(DefaultDeliveryStop defaultStop, string path)
        {
            
            //TODO: DO DIZ!
            
            
            defaultStop.Customers = DbCustomer.GetCustomers(defaultStop.ID);
        }
    }
}
