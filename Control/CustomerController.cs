
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Server;

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
        public void AddCustomers(DefaultDeliveryStop DefaultStop)
        {
            DefaultStop.Customers = (DbCustomer.GetCustomers(DefaultStop.ID));
        }
    }
}
