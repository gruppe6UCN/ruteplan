package database;

import model.Route;

import java.sql.SQLException;

/**
 * DBRoute
 * Handles all database functionality for routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBRoute {
    
    private DBConnection dbConnection;
    private static DBRoute instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBRoute() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBRoute getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBRoute();            
        }
        
        return instance;
    }

	
	/**
	 * Stores all routes in the database.
     * @param route list of all routes to store.
     */
	public void storeRoute(Route route) {
        String sql = String.format("INSERT into Route values(%d, '%s', '%s');",
                route.getDefaultRoute().getID(),
                route.getAuctualTimeOfDeparture().toString(),
                route.getDate().toString());
        long routeID = dbConnection.sendInsertSQL(sql);
        route.setID(routeID);
	}

}
