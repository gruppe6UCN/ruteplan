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
