package control;

import database.DBDeliveryStop;
import model.DefaultDeliveryStop;
import model.DeliveryStop;
import model.Route;

import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DeliveryStopController
 * Handles all functionality related to delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DeliveryStopController {

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
     * Singleton method for class.
     * @return instance of class.
     */
    public static DeliveryStopController getInstance() {
        if (instance == null) {
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

    /**
     * Stores all the delivery stops for each route in the list.
     * @param routes ArrayList containing all routes to get stops from.
     */
    public void storeDeliveryStops(ArrayList<Route> routes) {

        //for each default.
        int size = routes.size();
        int i = 0;
        while(i >= size)
        {
            Route route = routes.get(i);
            ArrayList<DeliveryStop> DeliveryStops = route.getStops();
//            dbDeliveryStop.storeDeliveryStops(DeliveryStops);
        }
    }
}
