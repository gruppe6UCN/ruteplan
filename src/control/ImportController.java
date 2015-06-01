package control;


import model.Route;

import java.time.LocalDate;
import java.util.List;
import java.util.Vector;

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
     * @param rowData
     */
    public void importRoutes(Vector<Vector> rowData) {
        routeController.importRoutes(LocalDate.now());

        List<Route> routes = routeController.getRoutes();

        routes.forEach(route -> {
            Vector row = new Vector();
            row.addElement(Long.valueOf(route.getID()).toString());
            row.addElement(Long.valueOf(route.getDefaultRoute().getID()).toString());
            row.addElement(Integer.valueOf(route.getStops().size()));
            rowData.add(row);
        });
    }
    
}
