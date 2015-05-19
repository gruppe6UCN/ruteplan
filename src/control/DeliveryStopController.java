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
	
	
	
	
}
