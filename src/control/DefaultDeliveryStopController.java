package control;

import database.DBDefaultDeliveryStop;
import model.DefaultDeliveryStop;
import model.DefaultRoute;

import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DefaultDeliveryStopController
 * Handles all functionality related to default delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DefaultDeliveryStopController {
    
    private DBDefaultDeliveryStop dbDefaultDeliveryStop;
    private CustomerController customerController;
    private static DefaultDeliveryStopController instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DefaultDeliveryStopController() {
        try {
            dbDefaultDeliveryStop = DBDefaultDeliveryStop.getInstance();
            customerController = CustomerController.getInstance();
        } catch (ClassNotFoundException | SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     */
    public static DefaultDeliveryStopController getInstance() {
        if (instance == null) {
            instance = new DefaultDeliveryStopController();            
        }
        
        return instance;
    }
    
    
    /**
     * Gets an ArrayList of all default delivery stops for the current route.
     * @return List of all defaultDeliveryStops.
     * @param defaultRoute The defaultRoute to find all defaultDeliveryStops for.
     * 
     */
    public ArrayList<DefaultDeliveryStop> getDefaultDeliveryStops(DefaultRoute defaultRoute) {
        
        long defaultRouteID = defaultRoute.getID();
        ArrayList<DefaultDeliveryStop> stops = dbDefaultDeliveryStop.getDefaultDeliveryStops(defaultRouteID);

        // foreach DefaultDeliveryStop the customers are added
        stops.parallelStream().forEach((stop) -> customerController.addCustomers(stop));  // TODO: make parallelStream

        return stops;
    }


}