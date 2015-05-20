package database;

import java.sql.SQLException;

import model.*;

/**
 * DBDefaultRoute
 * Handles all database functionality for default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBDefaultRoute {
	
	private DBConnection dbConnection;
	private static DBDefaultRoute instance;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DBDefaultRoute() throws ClassNotFoundException, SQLException {
		dbConnection = DBConnection.getInstance();		
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	public static DBDefaultRoute getInstance() throws ClassNotFoundException, SQLException {
		if (instance == null) {
			instance = new DBDefaultRoute();			
		}
		
		return instance;
	}

}
