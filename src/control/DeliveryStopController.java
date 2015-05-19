package control;

import java.util.ArrayList;

import model.*;

public class DeliveryStopController {
	
	private TransportUnitController transportUnitController;
	private static DeliveryStopController instance;
	
	/**
	 * Private constructor for singleton.
	 */
	private DeliveryStopController() {
		transportUnitController = TransportUnitController.getInstance();
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
			
			//Gets the transportUnits for the deliveryStop.
			transportUnitController.getTransportUnits(deliveryStop, customerID);
			
			
			
			//Adds deliveryStop to route.
			
			
			
			
			
			//Increments counter for next loop.
			i++;
		}
	}
	
	
	
	
}
