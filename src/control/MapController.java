package control;

import java.sql.SQLException;
import java.util.ArrayList;

import database.*;
import model.*;

/**
 * DefaultRouteController
 * Handles all functionality related to default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 22-05-15
 */

public class MapController {
	
	private static MapController instance;
	private DBGeoLoc dbGeoLoc;
	private DBRoad dbRoad;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private MapController() {
		try {
			dbGeoLoc = DBGeoLoc.getInstance();
		} catch (ClassNotFoundException | SQLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		try {
			dbRoad = DBRoad.getInstance();
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static MapController getInstance() {
		if (instance == null) {
			instance = new MapController();			
		}
		
		return instance;
	}	
	
	/**
	 * Loads all map data from the database.
	 */
	public void loadMaps(ArrayList<Route> routes) {
		
		//Creates an ArrayList for default stops.
		ArrayList<DefaultDeliveryStop> defaultStops = new ArrayList<>();		
		
		//Enters a loop for each route.
        routes.stream().forEach((route) -> {      	
        	
        	//Enters a loop for each delivery stop.
        	ArrayList<DeliveryStop> stops = route.getStops();       	
        	stops.stream().forEach((stop) -> {
        		
        		//Finds the default stop and adds it to ArrayList.
        		DefaultDeliveryStop defaultStop = stop.getDefaultStop();
        		defaultStops.add(defaultStop);
        		
        	});
        });

        //Loads all geoLocs from the database.
		dbGeoLoc.getGeoLocFor(defaultStops);
	
		
		
		
		
	}
	
	
	
}
