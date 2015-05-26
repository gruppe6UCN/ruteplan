package control;

import database.DBRoute;
import model.DefaultRoute;
import model.Route;
import model.DeliveryStop;
import model.TransportUnit;

import java.sql.SQLException;
import java.util.*;

/**
 * RouteController
 * Handles all functionality related to routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class RouteController {

    private DeliveryStopController deliveryStopController;
    private DefaultRouteController defaultRouteController;
    private DefaultDeliveryStopController defaultDeliveryStopController;
    private DBRoute dbRoute;
    private static RouteController instance;
    private ArrayList<Route> routes;
    private double load;

    /**
     * Private constructor for singleton.
     */
    private RouteController() {
        deliveryStopController = DeliveryStopController.getInstance();
        defaultRouteController = DefaultRouteController.getInstance();
        defaultDeliveryStopController = DefaultDeliveryStopController.getInstance();
        try {
            dbRoute = DBRoute.getInstance();
        } catch (ClassNotFoundException | SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static RouteController getInstance() {
        if (instance == null) {
            instance = new RouteController();
        }

        return instance;
    }

    /**
     * Imports all routes from database.
     */
    public void importRoutes(Date date) {

        // Sync List
        List<DefaultRoute> listDefaultRoutes = Collections.synchronizedList(
                //Gets a list of all defaultRoutes.
                defaultRouteController.getDefaultRoutes());

        //create a route for each defaultRoute
        listDefaultRoutes.parallelStream().forEach((defaultRoute -> {
            //Creates new routes for each defaultRoute.
            Route route = new Route(defaultRoute, date);

            //Sync then adding
            synchronized (listDefaultRoutes) {
                //Creates Delivery Stops
                deliveryStopController.addDeliveryStops(
                        route,
                        //Gets a list of all stops for the defaultRoute.
                        defaultDeliveryStopController.getDefaultDeliveryStops(defaultRoute)
                );
            }
        }));
    }

    /**
     * Exports all data to database.
     */
    public void exportData() {
        routes.parallelStream().forEach(route -> {
            if (route.getDefaultRoute().isExtraRoute()) {
                defaultRouteController.storeDefaultRoute(route.getDefaultRoute());
            }
            dbRoute.storeRoute(route);
            deliveryStopController.storeDeliveryStops(route);
        });
    }

    
	/**
	 * Finds and returns all overloaded routes.
	 * @return ArrayList containing all overloaded routes.
	 */
	public ArrayList<Route> findOverloadedRoutes() {
		
		//Creates an ArrayList for each overloaded route.
		ArrayList<Route> overloadedRoutes = new ArrayList<>();
		
		//Enters a loop for each route.
        routes.stream().forEach((route) -> {
        	
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
        	
        	//Checks to see if route is overloaded.
        	if (load > capacity) {
        		//Adds overloaded route to ArrayList.
        		overloadedRoutes.add(route);
        	}
        });
		
		//Return list with all overloaded routes.
		return overloadedRoutes;	
	}
	
	/**
	 * Finds and returns all under loaded routes.
	 * @return ArrayList containing all under loaded routes.
	 */
	public ArrayList<Route> findUnderloadedRoutes() {
		
		//Creates an ArrayList for each overloaded route.
		ArrayList<Route> underloadedRoutes = new ArrayList<>();
		
		//Enters a loop for each route.
        routes.stream().forEach((route) -> {
        	
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
        	
        	//Checks to see if route is under loaded.
        	if (load < capacity * 0.8) {
        		//Adds under loaded route to ArrayList.
        		underloadedRoutes.add(route);
        	}
        });
		
		//Return list with all overloaded routes.
		return underloadedRoutes;	
	}

	/**
	 * @return the routes
	 */
	public ArrayList<Route> getRoutes() {
		return routes;
	}
}
