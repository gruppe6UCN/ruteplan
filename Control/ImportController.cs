using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Control
{
    public class ImportController
    {
        private RouteController routeController;
        private static ImportController instance;

    /**
     * Private constructor for singleton.
     */
    private ImportController() {
        RouteController = RouteController.GetInstance();
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
            row.addElement(String.format("%03d", route.getDefaultRoute().getID()));
            row.addElement(route.getStops().size());
            row.addElement(String.format("%.1f / %.1f", route.getLoadForTrailer(), route.getCapacity()));
            rowData.add(row);
        });
    }
}
