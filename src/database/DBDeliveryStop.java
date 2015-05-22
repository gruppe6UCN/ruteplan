package database;

import java.sql.SQLException;
import java.util.ArrayList;

import model.*;

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

	public void storeDeliveryStops(ArrayList<DeliveryStop> deliveryStops) {
		// TODO Auto-generated method stub
		
	}

}
