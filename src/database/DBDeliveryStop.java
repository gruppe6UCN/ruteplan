package database;

import model.DeliveryStop;

import java.sql.SQLException;

/**
 * DBDeliveryStop
 * Handles all database functionality for delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBDeliveryStop {
    
    private DBConnection dbConnection;
    private static DBDeliveryStop instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBDeliveryStop() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBDeliveryStop getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBDeliveryStop();            
        }

        return instance;
    }

    /**
     *
     * @param routeID from Route
     * @param deliveryStop
     * @return the id for deliveryStop
     */
    public long storeDeliveryStops(long routeID, DeliveryStop deliveryStop) {
        String sql = String.format("INSERT into Route values(%d, %d);",
                routeID,
                deliveryStop.getID());
        return dbConnection.sendInsertSQL(sql);
    }

}
