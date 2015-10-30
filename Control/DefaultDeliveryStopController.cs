using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Server.Database;

namespace Control
{
    public class DefaultDeliveryStopController
    {
        public DBDefaultDeliveryStop DbDefaultDeliveryStop { get; private set; }
        public CustomerController CustomerCtr { get; private set; }
        private static DefaultDeliveryStopController instance;

        /**
         * Private constructor for singleton. 
         */
        private DefaultDeliveryStopController() 
        {
            DbDefaultDeliveryStop = DBDefaultDeliveryStop.Instance;
            CustomerCtr = CustomerController.Instance;
        }

        /**
         * Singleton method for class.
         * @return instance of class.
         */
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

        /**
         * Gets an ArrayList of all default delivery stops for the current route.
         * @return List of all defaultDeliveryStops.
         * @param defaultRoute The defaultRoute to find all defaultDeliveryStops for.
         * 
         */
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(DefaultRoute defaultRoute)
        {
            long defaultRouteID = defaultRoute.ID;
            List<DefaultDeliveryStop> stops = DbDefaultDeliveryStop.GetDefaultDeliveryStops(defaultRouteID);

            // foreach DefaultDeliveryStop the customers are added
            //TODO: Make some threads n sheit...
            foreach (DefaultDeliveryStop stop in stops) 
            {
                CustomerCtr.AddCustomers(stop);
            }

            return stops;
        }
    }
}
