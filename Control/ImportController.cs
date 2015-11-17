using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class ImportController
    {
        public RouteController RouteCtr { get; private set; }
        private static ImportController instance;

    /**
     * Private constructor for singleton.
     */
    private ImportController() {
        RouteCtr = RouteController.Instance;
    }

    /// <summary>
    /// Singleton method. Returns the instance of the class.
    /// </summary>
    /// <returns>Instance of class.</returns>
    public static ImportController Instance { 
            get { 
                if (instance == null)
                    instance = new ImportController();
                return instance;
            }
        }
    
    public void ImportRoutes()
    {
        RouteCtr.ImportRoutes(DateTime.Now);
    }


    /**
     * Imports all routes from database.
     * @param rowData
     *
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
     * 
     * */
    }
}
