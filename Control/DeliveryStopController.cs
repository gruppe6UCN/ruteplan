using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class DeliveryStopController
    {
        private TransportUnitController transportUnitController;
        private DBDeliveryStop dbDeliveryStop;
        private static DeliveryStopController instance;

        /**
         * Private constructor for singleton.
         */
        private DeliveryStopController() {
        transportUnitController = TransportUnitController.getInstance();
        try {
            dbDeliveryStop = DBDeliveryStop.getInstance();
        } catch (ClassNotFoundException | SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

        /**
         * Stores all the delivery stops for each route in the list.
         * @param route ArrayList containing all stop from a route stops from.
         */
        public void storeDeliveryStops(Route route) {
        route.getStops().parallelStream().forEach((stop) -> { // TODO: make parallelStream
            long deliveryStopID = dbDeliveryStop.store(route.getID(), stop);
            stop.setID(deliveryStopID);
        });
    }

        /**
         * Singleton method for class.
         * @return instance of class.
         */
        public static DeliveryStopController getInstance()
        {
            if (instance == null)
            {
                instance = new DeliveryStopController();
            }

            return instance;
        }


        /**
         * Adds all delivery defaultStops to the the route.
         * @param route route to add deliveryStops to.
         * @param defaultStops ArrayList of defaultDeliveryStops.
         */
        public void addDeliveryStops(Route route, ArrayList<DefaultDeliveryStop> defaultStops) {
        //for each DefaultDeliveryStop add one DeliveryStop to route
        defaultStops.stream().forEach((defaultStop) -> {

            DeliveryStop stop = new DeliveryStop(defaultStop);

            //add all TransportUnit for this DeliveryStop
            transportUnitController.addTransportUnit(stop, stop.getDefaultStop().getCustomers());

            //Adds deliveryStop to route.
            route.addDeliveryStop(stop);
        });
    }
    }
  
}
