package control;

import model.Route;

import java.util.List;
import java.util.Vector;

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
    public void exportDatas(Vector rowData) {
        routeController.exportData();

        List<Route> routes = routeController.getRoutes();

        routes.forEach(route -> {
            Vector row = new Vector();
            row.addElement(String.format("%03d", route.getID()));
            row.addElement(route.getDefaultRoute().isExtraRoute() ? "NONE" : String.format("%03d", route.getDefaultRoute().getID()));
            row.addElement(route.getStops().size());
            row.addElement(String.format("%.1f / %.1f", route.getLoadForTrailer(), route.getCapacity()));
            row.addElement(String.format("%02d:%02d", route.getTimeForDeparture().getHour(), route.getTimeForDeparture().getMinute()));
            row.addElement(route.getDefaultRoute().isExtraRoute() ? "Yes" : "No");
            rowData.add(row);
        });
    }
    
}
