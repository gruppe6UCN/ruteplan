package database;

import java.sql.SQLException;

/**
 * DBRoad
 * Handles all database functionality for roads.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBRoad {
    
    private DBConnection dbConnection;
    private static DBRoad instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBRoad() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBRoad getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBRoad();            
        }

        return instance;
    }
}