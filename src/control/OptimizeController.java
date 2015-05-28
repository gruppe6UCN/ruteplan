package control;

import model.*;

import java.sql.Date;
import java.sql.Time;
import java.util.ArrayList;
import java.util.List;

/**
 * OptimizeController
 * Handles all functionality for the use-case optimize.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 27-05-15
 */

public class OptimizeController {
    private RouteController routeController;
    private MapController mapController;
    private static OptimizeController instance;
    private double load;
    private ArrayList<DeliveryStop> removedStops;
    private boolean isnear;
    private double bestdistance;
    private Route bestRoute;
    
    /**
     * Private constructor for singleton.
     */
    private OptimizeController() {
        
        routeController = RouteController.getInstance();
        mapController = MapController.getInstance();
        removedStops = new ArrayList<>();
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
        List<Route> allRoutes = routeController.getRoutes();
        
        //Loads maps.
        mapController.loadMaps(allRoutes);
            
        //Checks to see if there is overloadedRoutes.
        if (overloadedRoutes.size() >= 1) {
            
            //Enters a loop for each overloaded route.
            overloadedRoutes.stream().forEach((overloadedRoute) -> {
                
                //Finds overloaded amount.
                double overload = findOverloadAmount(overloadedRoute);
                
                //Removes deliveryStops from route, until it's not overloaded, using a greedy algorithm.
                while (overload > 0) {
                    
                    //Finds the most/best overloaded stop.
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
                    
                    //ArrayList containing all nearRoutes.
                    ArrayList<Route> nearRoutes = new ArrayList<>();
                    
                    //Enter a loop for each stop removed.
                    removedStops.stream().forEach((removedStop) -> {
                        
                        //Finds the geoLoc for current removedStop.
                        GeoLoc geoLocRemovedStop = mapController.findGeoLoc(removedStop);
                        
                        //Enters a loop for each underloadedRoute, to check if one of them is near stop.
                        underloadedRoutes.stream().forEach((underloadedRoute) -> {
                            
                            //Misc Variables for the loop.
                            boolean isnear_route = false;
                            double bestdistance = 9999;
                            
                            //Enters a loop for each stop.
                            ArrayList<DeliveryStop> stops = underloadedRoute.getStops();
                            for(DeliveryStop deliveryStop:stops) {
                                
                                //Gets the geoLoc, and checks to see if one of those are near the removed stop.
                                GeoLoc geoLoc = mapController.findGeoLoc(deliveryStop);
                                double distance = mapController.pointDistance(geoLoc, geoLocRemovedStop);
                                
                                //Checks to see if within a certain distance.
                                if (distance < 25) {
                                    
                                    //Checks to see if shorter then best distance.
                                    if (distance < bestdistance) {
                                        
                                        //Sets to the new best distance.
                                        bestdistance = distance;
                                        isnear_route = true;
                                        isnear = true;
                                    }
                                }
                            }
                        
                            //Adds the underloadedRoute to ArrayList of nearRoutes.
                            if (isnear_route) {
                                nearRoutes.add(underloadedRoute);
                            }
                        });
                        
                        if (isnear) {
                            
                            //Checks to see if more than one are near the stop.
                            int nearRouteQuantity = nearRoutes.size();
                            if (nearRouteQuantity > 1) {
                                
                                //Best distance variable.
                                bestdistance = 9999;
                                bestRoute = nearRoutes.get(0);
                                
                                //Enters a loop for each route to find the best.
                                nearRoutes.stream().forEach((nearRoute) -> {
                                    
                                    //Enters a loop for each stop.                                
                                    ArrayList<DeliveryStop> stops = nearRoute.getStops();
                                    for(DeliveryStop deliveryStop:stops) {
                                        
                                        //Gets the geoLoc and distance of the current stop.
                                        GeoLoc geoLoc = mapController.findGeoLoc(deliveryStop);
                                        double distance = mapController.pointDistance(geoLoc, geoLocRemovedStop);
                                        
                                        //Checks to see if better then best distance.
                                        if (distance < bestdistance) {
                                            bestdistance = distance;
                                            bestRoute = nearRoute;
                                        }
                                    }
                                });

                                //Move stop to this route.
                                Route underloadedRoute = nearRoutes.get(0);
                                underloadedRoute.addDeliveryStop(removedStop);
                            }
                            else {
                                
                                //Move stop to this route.
                                Route underloadedRoute = nearRoutes.get(0);
                                underloadedRoute.addDeliveryStop(removedStop);
                            }
                        }
                        else {
                            
                            //Make a new route.
                            Route newRoute = newRoute(removedStops);
                            underloadedRoutes.add(newRoute);
                            allRoutes.add(newRoute);
                        }
                    });
                }
                else {
                    
                    //Make a new route.
                    Route newRoute = newRoute(removedStops);
                    underloadedRoutes.add(newRoute);
                    allRoutes.add(newRoute);
                }
            });  
            
            //Updates all routes again.
            routeController.setRoutes(allRoutes);
            
            //Clears ArrayList
            removedStops.clear();
    }
}
	
	
	/**
	 * Makes a new route with the given delivery stops.
	 * @param deliveryStops
	 * @return returns the route made.
	 */
	private Route newRoute(ArrayList<DeliveryStop> deliveryStops) {
		
		//Create default route.
		Time timeOfDeparture = new Time(3, 0, 0);
		TrailerType trailerType = TrailerType.STOR;
		boolean extraRoute = true;
		
		DefaultRoute defaultRoute = new DefaultRoute(timeOfDeparture, trailerType, extraRoute);
		
		//Add default stops.
		for(DeliveryStop deliveryStop:deliveryStops) {
			
			//Gets the default stop from normal stop and adds it to defaultRoute.
			DefaultDeliveryStop defaultStop = deliveryStop.getDefaultStop();
			defaultRoute.addDefaultDeliveryStop(defaultStop);	
		}
		
		//Create route.
		Date date = new Date(0, 0, 0);
		Route route = new Route(defaultRoute, date);
		
		//Add stops.
		route.setStops(deliveryStops);
		
		//Return route.
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
    			load += transportUnit.getUnitType().getSize();
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
			load += transportUnit.getUnitType().getSize();
		}
		
		//Returns the load.
		return load;
	}
}