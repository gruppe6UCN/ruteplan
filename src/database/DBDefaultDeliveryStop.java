package database;

import java.sql.SQLException;

import model.*;

/**
 * DBDefaultDeliveryStop
 * Handles all database functionality for default delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBDefaultDeliveryStop {
	
	private DBConnection dbConnection;
	private static DBDefaultDeliveryStop instance;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DBDefaultDeliveryStop() throws ClassNotFoundException, SQLException {
		dbConnection = DBConnection.getInstance();		
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	public static DBDefaultDeliveryStop getInstance() throws ClassNotFoundException, SQLException {
		if (instance == null) {
			instance = new DBDefaultDeliveryStop();			
		}
		
		return instance;
	}
	
	

}
