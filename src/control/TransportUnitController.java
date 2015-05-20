package control;

import java.util.ArrayList;

import database.*;
import model.*;

/**
 * TransportUnitController
 * Handles all functionality related to transport units.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 19-05-15
 */

public class TransportUnitController {
	
	private DBTransportUnit dbTransportUnit;
	private static TransportUnitController instance;
	
	/**
	 * Private constructor for singleton.
	 */
	private TransportUnitController() {
		dbTransportUnit = DBTransportUnit.getInstance();
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static TransportUnitController getInstance() {
		if (instance == null) {
			instance = new TransportUnitController();			
		}
		
		return instance;
	}
	
	/**
	 * Adds a transport unit to the delivery stop.
	 * @param deliveryStop DeliveryStop to have transport units added.
	 * @param customerID ID of the customer whose transports units are to be added.
	 */
	public void addTransportUnit(DeliveryStop deliveryStop, long customerID) {
		
		//Gets a list of all transportUnits for the customer.
		ArrayList<TransportUnit> list = dbTransportUnits.getTransportUnits(customerID);
		
		//Adds each transport unit to the deliveryStop.
		int size = list.size();
		int i = 0;
		while(i >= size)
		{
			TransportUnit transportUnit = list.get(i);
			deliveryStop.addTransportUnit(transportUnit);
			i++;
		}
	}
	
	
	
	
}
