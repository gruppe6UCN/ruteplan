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
	        		DeliveryStop best = findBestOverloadedStop(overloadedRoute, overload);
	        		
	        		//Removes stop from route.
	        		overloadedRoute.getStops().remove(best);
	        		
	        		//Adds stop to ArrayList.
	        		removedStops.add(best);
	        		
	        		//Decrements overload.
	        		overload -= findLoad(best);
	        	}
	        	
	        	//Checks to see if there is underloadedRoutes.
				if (underloadedRoutes.size() >= 1) {
					
					//Enter a loop for each stop removed.
					removedStops.stream().forEach((removedStop) -> {
						
						
						
						//Check to see if one of the underloadedRoutes are near stop.
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
	 * @return returns the route made.
	 */
	private Route newRoute(ArrayList<DeliveryStop> deliveryStops) {
		
		Route route = new Route(null, null);
		
		
		
		
		
		
		
		
		
		
		
		
		
		return route;
	}
	
	
	
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
	 * Finds the most overloaded delivery stop in the given route, as long as it doesn't exceed the overload cap.
	 * If no such stop can be found, finds the least overloaded route instead.
	 * @param route the route whose stops are to be checked.
	 * @param overload the overload cap to exclude stops whose load are not optimal.
	 * @return the best suited deliveryStop whose load most closely fits the cap.
	 */
	private DeliveryStop findBestOverloadedStop(Route route, double overload) {
		
		//Finds an initial stops for comparison.
		DeliveryStop biggest = route.getStops().get(0);
		DeliveryStop best = biggest;
		
		//Boolean to check if cap is exceeded.
		boolean exceedCap = false;
		
		//Enters a loop for each stop, to find the most overloaded stop.
		ArrayList<DeliveryStop> stops = route.getStops();
		for(DeliveryStop deliveryStop:stops) {
			
			//Compares the load of the deliveryStop with the load of the biggest, to find which is biggest.
			double biggestload = findLoad(biggest);
			double compareload = findLoad(deliveryStop);
			if (compareload > biggestload) {
				
				//Sets the biggest to the current.
				biggest = deliveryStop;
				best = biggest;
				
				//Checks to see if the biggest exceeds the cap.
				if (biggestload > overload) {
					exceedCap = true;
				}
				else{
					exceedCap = false;
				}
			}
		}
		
		//Checks to see if the biggest exceeds the limit.
		if (exceedCap) {
			
			//Initial stop for comparison.
			DeliveryStop smallest = route.getStops().get(0);
			
			//Enters a loop for each stop, to find the least overloaded stop.
			for(DeliveryStop deliveryStop:stops) {
				
				//Compares the load of the deliveryStop with the load of the smallest, to find which is smallest.
				double smallestload = findLoad(smallest);
				double compareload = findLoad(deliveryStop);
				if (compareload < smallestload) {
					
					//Sets the smallest to current.
					smallest = deliveryStop;
					best = smallest;
				}
			}	
		}
		
		//Returns the best load.
		return best;
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