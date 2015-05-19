package control;

import database.*;

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
	
	

}
