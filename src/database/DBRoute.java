package database;

import java.sql.SQLException;

import model.*;

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

}
