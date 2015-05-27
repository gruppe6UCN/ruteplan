package control;


import java.sql.ResultSet;

/**
 * ImportController
 * Handles all functionality for the use-case import.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class ImportController {

    private RouteController routeController;
    private static ImportController instance;

    /**
     * Private constructor for singleton.
     */
    private ImportController() {
        routeController = RouteController.getInstance();
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static ImportController getInstance() {
        if (instance == null) {
            instance = new ImportController();
        }

        return instance;
    }
    
    /**
     * Imports all routes from database.
     */
    public ResultSet importRoutes() {
        routeController.importRoutes(new java.util.Date());

        ResultSet resultSet;

        return null;
    }
    
}
