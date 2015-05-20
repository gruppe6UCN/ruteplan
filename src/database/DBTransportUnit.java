package database;

import java.sql.SQLException;

import model.*;

/**
 * DBTransportUnit
 * Handles all database functionality for default transport units.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBTransportUnit {
	
	private DBConnection dbConnection;
	private static DBTransportUnit instance;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DBTransportUnit() throws ClassNotFoundException, SQLException {
		dbConnection = DBConnection.getInstance();		
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	public static DBTransportUnit getInstance() throws ClassNotFoundException, SQLException {
		if (instance == null) {
			instance = new DBTransportUnit();			
		}
		
		return instance;
	}

}
