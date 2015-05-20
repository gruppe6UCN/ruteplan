package control;

import java.sql.SQLException;
import java.util.ArrayList;

import model.*;
import database.*;

/**
 * DeliveryStopController
 * Handles all functionality related to delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DeliveryStopController {
	
	private TransportUnitController transportUnitController;
	private DBDeliveryStop dbDeliveryStop;
	private static DeliveryStopController instance;
	
	/**
	 * Private constructor for singleton.
	 */
	private DeliveryStopController() {
		transportUnitController = TransportUnitController.getInstance();
		try {
			dbDeliveryStop = DBDeliveryStop.getInstance();
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static DeliveryStopController getInstance() {
		if (instance == null) {
			instance = new DeliveryStopController();			
		}
		
		return instance;
	}

	
	/**
	 * Adds all delivery stops to the the route.
	 * @param route route to add deliveryStops to.
	 * @param stops ArrayList of defaultDeliveryStops.
	 */
	public void addDeliveryStops(Route route, ArrayList<DefaultDeliveryStop> stops) {
		
		//Enters a while loop for each delivery stop.
		int size = stops.size();
		int i = 0;
		while(i >= size)
		{
			//Creates the deliveryStop.
			DefaultDeliveryStop defaultDeliveryStop = stops.get(i);
			DeliveryStop deliveryStop = new DeliveryStop(defaultDeliveryStop);
			
			//Gets each transportUnits for the deliveryStop.
			int size2 = defaultDeliveryStop.getCustomers().size();
			int ii = 0;
			while(ii >= size2) 
			{
				long customerID = defaultDeliveryStop.getCustomers().get(ii).getId();
				transportUnitController.addTransportUnit(deliveryStop, customerID);
				ii++;
			}
			
			//Adds deliveryStop to route.
			route.addDeliveryStop(deliveryStop);
			
			//Increments counter for next loop.
			i++;
		}
	}
	
	/**
	 * Stores all the delivery stops for each route in the list.
	 * @param routes ArrayList containing all routes to get stops from.
	 */
	public void storeDeliveryStops(ArrayList<Route> routes) {
		
		//Enters a while loop for each route.
		int size = routes.size();
		int i = 0;		
		while(i >= size)
		{
			Route route = routes.get(i);
			ArrayList<DeliveryStop> DeliveryStops = route.getStops();
			dbDeliveryStop.storeDeliveryStops(DeliveryStops);
		}	
	}
	
	
	
}
