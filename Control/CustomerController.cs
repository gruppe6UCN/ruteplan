using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Model;

namespace Control
{
    class CustomerController
    {
        private DBCustomer DbCustomer { set; get; }
        private static CustomerController instance;

        /**
         * Private constructor for singleton.
         */
        private CustomerController()
        {
            DbCustomer = DBCustomer.Instance;
        }

        /**
         * Singleton method for class.
         * @return instance of class.
         */
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
