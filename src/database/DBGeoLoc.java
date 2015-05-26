package database;

import java.sql.SQLException;
import java.util.ArrayList;
import model.*;

/**
 * DBGeoLoc
 * Handles all database functionality for GeoLocs.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBGeoLoc {
    
    private DBConnection dbConnection;
    private static DBGeoLoc instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBGeoLoc() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBGeoLoc getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBGeoLoc();            
        }

        return instance;
    }

	public void getGeoLocFor(ArrayList<DefaultDeliveryStop> defaultStops) {
		// TODO Auto-generated method stub
		
	}
}