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
	private double load;
	private ArrayList<DeliveryStop> removedStops;
    
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
	 * Optimizes all imported routes.
	 */
	public void optimize() {
		
		//Finds all needed routes.
		ArrayList<Route> overloadedRoutes = routeController.findOverloadedRoutes();
		ArrayList<Route> underloadedRoutes = routeController.findUnderloadedRoutes();
		ArrayList<Route> allRoutes = routeController.getRoutes();
		
		//Loads maps.
		mapController.loadMaps(allRoutes);
		
		
		
		//Optimizes the routes.
		//doMath();
		
		
		//Checks to see if there is overloadedRoutes.
		if (overloadedRoutes.size() >= 1) {
			
			//Enters a loop for each overloaded route.
	        overloadedRoutes.stream().forEach((overloadedRoute) -> {
	        	
	        	//Finds overloaded amount.
	        	double overload = findOverloadAmount(overloadedRoute);
	        	
	        	//Removes deliveryStops from route, until it's not overloaded, using a greedy algorithm.
	        	while (overload > 0) {
	        		
	        		//Finds the most overloaded stop.
	        		DeliveryStop most = findMostOverloaded(overloadedRoute);
	        		
	        		//Removes stop from route.
	        		overloadedRoute.getStops().remove(most);
	        		
	        		//Adds stop to ArrayList.
	        		removedStops.add(most);
	        		
	        		//Decrements overload.
	        		overload -= findLoad(most);
	        	}
	        	
	        	//Checks to see if there is underloadedRoutes.
				if (underloadedRoutes.size() >= 1) {
					
					//Enter a loop for each stop removed.
					removedStops.stream().forEach((route) -> {
						
						//Check to see if one of those routes are near stop.
						if (1 + 1 == 2) {
							
							
							
							
							//Checks to see if more than one are near stop.
							if (1+1==2) {
								
								
								//Find the best route.
								
								
								
								//Move stop to that route.
								
								
								
								
							}
							else {
								
								//Move stop to this route.
								
								
								
							}
							
							
							
						}
						else {
							
							//Make a new route.
							
							
						}
			        	
			        });
					
				}
				else {
					
					//Make a new route.
					
					
				}
			});   
	}
}
	
	

	
	/**
	 * Makes a new route with the given delivery stops.
	 * @param deliveryStops
	 * @return
	 */
	//private Route newRoute(ArrayList<DeliveryStop> deliveryStops) {
		
		//return 
		
		
		
	//}
	
	
	
	/**
	 * Finds the amount overloaded for the route.
	 * @param route route to find overload amount for.
	 * @return the amount overloaded in double.
	 */
	private double findOverloadAmount(Route route) {
		
		//Variable to increment for each load check.
    	load = 0;
    	
    	//Finds maximum load.
    	double capacity = route.getDefaultRoute().getTrailerType().getCapacity();       	
    	
    	//Enters a loop for each delivery stop.
    	ArrayList<DeliveryStop> stops = route.getStops();       	
    	stops.stream().forEach((stop) -> {
    		
    		//Enters a loop for each transportUnit
    		ArrayList<TransportUnit> transportUnits = stop.getTransportUnits();
    		for(TransportUnit transportUnit:transportUnits) {
    			
    			//Increments load with the transportUnits size.
    			load += transportUnit.getType().getSize();
    		}
    	});
    	
    	//Finds amount overloaded, and returns that.
    	double overload = load - capacity;
		return overload;
	}
	
	
	
	/**
	 * Finds the most overloaded delivery stop in the given route.
	 * @param route whose stops are to be checked.
	 * @return the deliveryStop most overloaded.
	 */
	private DeliveryStop findMostOverloaded(Route route) {
		
		//Finds an initial stop for comparison.
		DeliveryStop current = route.getStops().get(0);
		
		//Enters a loop for each stop.
		ArrayList<DeliveryStop> stops = route.getStops();
		for(DeliveryStop deliveryStop:stops) {
			
			//Compares the load of the deliveryStop with the load of the current,
			//to find which is biggest. Sets the biggest of the two to current.
			double currentload = findLoad(current);
			double compareload = findLoad(deliveryStop);
			
			if (compareload > currentload) {
				current = deliveryStop;
			}
		}
		
		//Returns the most overloaded stop.
		return current;
	}
	
	/**
	 * Finds the load of the given delivery stop.
	 * @param stop stop to find load of.
	 * @return the load in double.
	 */
	private double findLoad(DeliveryStop stop) {
		
		//Creates variable.
		double load = 0;
		
		//Enters a loop for each transportUnit
		ArrayList<TransportUnit> transportUnits = stop.getTransportUnits();
		for(TransportUnit transportUnit:transportUnits) {
			
			//Increments load with the transportUnits size.
			load += transportUnit.getType().getSize();
		}
		
		//Returns the load.
		return load;
	}
}