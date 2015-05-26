package control;

import java.util.ArrayList;

import model.*;

/**
 * OptimizeController
 * Handles all functionality for the use-case optimize.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 26-05-15
 */

public class OptimizeController {
	
	private RouteController routeController;
	private MapController mapController;
	private static OptimizeController instance;
    
    /**
     * Private constructor for singleton.
     */
    private OptimizeController() {
		
		routeController = RouteController.getInstance();
		mapController = MapController.getInstance();
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static OptimizeController getInstance() {
		if (instance == null) {
			instance = new OptimizeController();			
		}
		
		return instance;
	}
	
	
	/**
	 * Optimizes all loaded routes.
	 */
	public void optimize() {
		
		//Finds over/under loaded routes.
		ArrayList<Route> overloadedRoutes = routeController.findOverloadedRoutes();
		ArrayList<Route> underloadedRoutes = routeController.findUnderloadedRoutes();
		
		//Loads maps.
		
		
		
		//Find and return overloaded routes.
		
		
		//Find and return underloaded routes.
		
		
		//Load Maps
		
		
		//Do dat math yo
		
		
		//Create new routes n delivery stops
		
		//Add extra routes.
		
		
		
		
		
		
		
		//Update routes
		
	}
}