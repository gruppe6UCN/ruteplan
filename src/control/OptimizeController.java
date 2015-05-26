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
		
		//Finds all needed routes.
		ArrayList<Route> overloadedRoutes = routeController.findOverloadedRoutes();
		ArrayList<Route> underloadedRoutes = routeController.findUnderloadedRoutes();
		ArrayList<Route> allRoutes = routeController.getRoutes();
		
		//Loads maps.
		mapController.loadMaps(allRoutes);
		
		
		
		//Optimizes the routes.
		doMath();
		
		
		//Checks to see if there is overloadedRoutes.
		if (overloadedRoutes.size() >= 1) {
			
			//Checks to see if there is underloadedRoutes.
			if (underloadedRoutes.size() >= 1) {
				
				//Removes customers from route, until it's not overloaded.
				
				
				
				
				//Enter a loop for each customer removed.
				
				
				
				
				
				
				//Check to see if one of those routes are near customer.
				if (1 + 1 == 2) {
					
					//Checks to see if more then one are near customer.
					if (1+1==2) {
						
						//Find the best route.
						
						//Move customer to that route.
						
					}
					else {
						
						//Move customer to this route.
						
					}
					
					
					
				}
				else {
					
					//Make a new route.
					
					
				}
				
					
			}
			else {
				
				//Make a new route.
				
				
			}
			
			
		}
	}
	
	
	
	
	
	
	/**
	 * Optimizes the routes to be awesome.
	 */
	private void doMath() {
		
		
	}
	
	
	
}