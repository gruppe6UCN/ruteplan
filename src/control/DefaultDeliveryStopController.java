package control;

import java.sql.SQLException;
import java.util.ArrayList;

import model.DefaultDeliveryStop;
import model.DefaultRoute;
import database.*;

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
	private static DefaultDeliveryStopController instance;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DefaultDeliveryStopController() {
		try {
			dbDefaultDeliveryStop = DBDefaultDeliveryStop.getInstance();
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
		
		long defaultRouteID = defaultRoute.getId();
		ArrayList<DefaultDeliveryStop> stops = dbDefaultDeliveryStop.getDefaultDeliveryStops(defaultRouteID);
		return stops;
	}

	
}