package control;

/**
 * ExportController
 * Handles all functionality for the use-case export.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class ExportController {
    
    private RouteController routeController;
    private static ExportController instance;
    
    /**
     * Private constructor for singleton.
     */
    private ExportController() {
        routeController = RouteController.getInstance();
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     */
    public static ExportController getInstance() {
        if (instance == null) {
            instance = new ExportController();            
        }
        
        return instance;
    }
    
    /**
     * Exports all routes to database.
     */
    public void exportDatas() {
        routeController.exportData();
    }
    
}
