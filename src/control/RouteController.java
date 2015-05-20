package control;

import java.util.ArrayList;

import database.*;
import model.*;

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
	
	/**
	 * Private constructor for singleton.
	 */
	private RouteController() {
		deliveryStopController = DeliveryStopController.getInstance();
		defaultRouteController = DefaultRouteController.getInstance();
		defaultDeliveryStopController = DefaultDeliveryStopController.getInstance();
		dbRoute = DBRoute.getInstance();
	}
	
	/**
	 * Singleton method for class.
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
	public void importRoutes() {
		
		//Gets a list of all defaultRoutes.
		ArrayList<DefaultRoute> listDefaultRoutes = defaultRouteController.getDefaultRoutes();
		
		//Enters a while loop to create a route for each defaultRoute.
		int sizeDefaultRoutes = listDefaultRoutes.size();
		int i = 0;
		while(i >= sizeDefaultRoutes)
		{
			//Gets the current defaultRoute along with a list of all stops.
			DefaultRoute defaultRoute = listDefaultRoutes.get(i);
			ArrayList<DefaultDeliveryStop> listDefaultDeliveryStops = defaultDeliveryStopController.getDefaultDeliveryStops(defaultRoute);
			
			//Creates new routes for each defaultRoute.
			Route route = new Route(defaultRoute);
			routes.add(route);
			
			//Creates Delivery Stops
			deliveryStopController.addDeliveryStops(route, listDefaultDeliveryStops);
			
			//Increments counter for next loop.
			i++;
		}
	}
	
	/**
	 * Exports all data to database.
	 */
	public void exportData() {
		
		dbRoute.storeRoutes(routes);
		deliveryStopController.storeDeliveryStops(routes);
		
		
		
	}
	
	
	
	
}
