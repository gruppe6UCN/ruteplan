package control;

import java.util.ArrayList;

import database.*;
import model.*;

public class DefaultRouteController {
	
	private DBDefaultRoute dbDefaultRoute;
	private static DefaultRouteController instance;
	
	/**
	 * Private constructor for singleton.
	 */
	private DefaultRouteController() {
		dbDefaultRoute = DBDefaultRoute.getInstance();
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
