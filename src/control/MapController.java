package control;

import java.sql.SQLException;

import database.*;

/**
 * DefaultRouteController
 * Handles all functionality related to default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 22-05-15
 */

public class MapController {
	
	private static MapController instance;
	private DBGeoLoc dbGeoLoc;
	private DBRoad dbRoad;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private MapController() {
		try {
			dbGeoLoc = DBGeoLoc.getInstance();
		} catch (ClassNotFoundException | SQLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		try {
			dbRoad = DBRoad.getInstance();
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static MapController getInstance() {
		if (instance == null) {
			instance = new MapController();			
		}
		
		return instance;
	}	
	
	
	
	
	/**
	 * Loads all map data from the database.
	 */
	private void loadMaps() {
		
		
		
		
	}
	
	
	
}
