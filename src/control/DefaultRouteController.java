package control;

import java.sql.SQLException;
import java.util.ArrayList;

import database.*;
import model.*;

/**
 * DefaultRouteController
 * Handles all functionality related to default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DefaultRouteController {
	
	private DBDefaultRoute dbDefaultRoute;
	private static DefaultRouteController instance;
	
	/**
	 * Private constructor for singleton.
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DefaultRouteController() {
		try {
			dbDefaultRoute = DBDefaultRoute.getInstance();
		} catch (ClassNotFoundException | SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static DefaultRouteController getInstance() {
		if (instance == null) {
			instance = new DefaultRouteController();			
		}
		
		return instance;
	}
	
	/**
	 * Gets an ArrayList of all defaultRoutes from the database.
	 * @return List of all defaultRoutes.
	 */
	public ArrayList<DefaultRoute> getDefaultRoutes() {
		
		//Gets a list of all defaultRoutes.
		ArrayList<DefaultRoute> list = dbDefaultRoute.getDefaultRoutes();
		
		//Returns the list.
		return list;
	}
	
	
	
}
