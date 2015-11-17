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

        /**
         * Private constructor for singleton.
         */
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
            defaultStop.Customers = (DbCustomer.GetCustomers(defaultStop.ID));
        }
    }
}
