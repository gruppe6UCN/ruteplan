using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Model;

namespace Control
{
    public class DeliveryStopController
    {
        private TransportUnitController TransportUnitCtr { get; private set; }
        private DBDeliveryStop DbDeliveryStop { get; private set; }
        private static DeliveryStopController instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DeliveryStopController() 
        {
            TransportUnitCtr = TransportUnitController.getInstance();
            DbDeliveryStop = DBDeliveryStop.Instance;
        }

        /**
         * Stores all the delivery stops for each route in the list.
         * @param route ArrayList containing all stop from a route stops from.
         */
        public void StoreDeliveryStops(Route route) {

        route.getStops().parallelStream().forEach((stop) -> { // TODO: make parallelStream
            long deliveryStopID = DbDeliveryStop.store(route.getID(), stop);
            stop.setID(deliveryStopID);
        });
    }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DeliveryStopController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DeliveryStopController();
                return instance;
            }
        }


        /**
         * Adds all delivery defaultStops to the the route.
         * @param route route to add deliveryStops to.
         * @param defaultStops ArrayList of defaultDeliveryStops.
         */
        public void addDeliveryStops(Route route, List<DefaultDeliveryStop> defaultStops) {
        //for each DefaultDeliveryStop add one DeliveryStop to route
        defaultStops.stream().forEach((defaultStop) -> {

            DeliveryStop stop = new DeliveryStop(defaultStop);

            //add all TransportUnit for this DeliveryStop
            TransportUnitCtr.addTransportUnit(stop, stop.getDefaultStop().getCustomers());

            //Adds deliveryStop to route.
            route.addDeliveryStop(stop);
        });
    }
    }
  
}
