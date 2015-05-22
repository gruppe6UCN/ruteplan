package control;

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
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private MapController() {
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
}
