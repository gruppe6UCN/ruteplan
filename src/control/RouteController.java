package control;

import java.util.Date;
import java.sql.SQLException;
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

        //Gets a list of all defaultRoutes.
        ArrayList<DefaultRoute> listDefaultRoutes = defaultRouteController.getDefaultRoutes();

        //create a route for each defaultRoute
        listDefaultRoutes.stream().forEach((defaultRoute -> {
            //Creates new routes for each defaultRoute.
            Route route = new Route(defaultRoute, date);

            //Creates Delivery Stops
            deliveryStopController.addDeliveryStops(
                    route,
                    //Gets a list of all stops for the defaultRoute.
                    defaultDeliveryStopController.getDefaultDeliveryStops(defaultRoute)
            );


        }));
    }

    /**
     * Exports all data to database.
     */
    public void exportData() {

        dbRoute.storeRoutes(routes);
        deliveryStopController.storeDeliveryStops(routes);

    }

    
	/**
	 * Finds and returns all overloaded routes.
	 * @return ArrayList containing all overloaded routes.
	 */
	public ArrayList<Route> findOverloadedRoutes() {
		
		//Enters a loop for each route.
        routes.stream().forEach((route) -> {
        	
        	//Finds maximum load. Temp disabled until enum have been created.
        	double capacity = route.getDefaultRoute().getTrailerType().getCapacity();
        	
        	
        	
        	
        });
		
		
		
		//Finds the trailer type size.
		
		
		
		
		//Enters a loop for each delivery stop.
		
		
		//Enters a loop for each transportUnit.
		
		
		//Finds the transport unit type/size
		
		
		//Checks to see if overloaded
		
		//Add route to arraylist if is overloaded.
		
		//Repeat steps until done.
		
		//Return list with all overloaded stuffs
		return routes; //PH
		
	}
}
