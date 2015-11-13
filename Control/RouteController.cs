using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Model;

namespace Control
{
    public class RouteController
    {
    public DeliveryStopController DeliveryStopCtr { get; private set; }
    public DefaultRouteController DefaultRouteCtr { get; private set; }
    public DefaultDeliveryStopController DefaultDeliveryStopCtr { get; private set; }
    public LogController LogCtr { get; private set; }
    public DBRoute DbRoute { get; private set; }
    
    private static RouteController instance;
    private List<Route> Routes = new List<Route>(); //TODO: Syncrhonizzedd collectfkinzion...



    /**
     * Private constructor for singleton.
     */
    private RouteController() {
        DeliveryStopCtr = DeliveryStopController.Instance;
        DefaultRouteCtr = DefaultRouteController.Instance;
        DefaultDeliveryStopCtr = DefaultDeliveryStopController.Instance;
        LogCtr = LogController.Instance;
        DbRoute = DBRoute.Instance;
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static RouteController Instance
    {
        get
        {
            if (instance == null)
                instance = new RouteController();
            return instance;
        }
    }

    /**
     * Imports all routes from database.
     */
    public void ImportRoutes(DateTime date) {
        
        // Remove old data
        Routes.Clear();

        // Sync List
        List<DefaultRoute> listDefaultRoutes = Collections.synchronizedList(
                //Gets a list of all defaultRoutes.
                DefaultRouteCtr.GetDefaultRoutes());
        LogCtr.StatusLog("Loaded defualt routes");

        //create a route for each defaultRoute
        listDefaultRoutes.parallelStream().forEach((defaultRoute -> { // TODO: make parallelStream
            //Creates new routes for each defaultRoute.
            Route route = new Route(defaultRoute, date);
            LogCtr.StatusLog("Creates new route, base on default route " + defaultRoute.getID());

            //Sync then adding
            synchronized (listDefaultRoutes) {
                //Creates Delivery Stops
                DeliveryStopCtr.addDeliveryStops(
                        route,
                        //Gets a list of all stops for the defaultRoute.
                        DefaultDeliveryStopCtr.getDefaultDeliveryStops(defaultRoute)
                );
            }
            LogCtr.StatusLog("Created new route from default route " + defaultRoute.getID());

            Routes.add(route);
        }));
    }

    /**
     * Exports all data to database.
     */
    public void exportData() {

        Routes.forEach(route -> {
            if (route.getDefaultRoute().isExtraRoute()) {
                DefaultRouteCtr.store(route.getDefaultRoute());

            }
            DbRoute.storeRoute(route);
            DeliveryStopCtr.storeDeliveryStops(route);

            LogCtr.StatusLog(String.format("Exported %sroute %d to database",
                    route.getDefaultRoute().isExtraRoute() ? "Extra " : "",
                    route.getID()));
        });
    }

    
    /**
     * Finds and returns all overloaded routes.
     * @return ArrayList containing all overloaded routes.
     */
    public List<Route> findOverloadedRoutes() {
        
        // Sync List
        List<Route> overloadedRoutes = Collections.synchronizedList(
                //Creates an ArrayList for each overloaded route.
                new ArrayList<>());
        
        //Enters a loop for each route.
        Routes.parallelStream().forEach((route) -> { // TODO: make parallelStream

            //Variable to increment for each load check.
            double load = route.getLoadForTrailer();

            //Finds maximum load.
            double capacity = route.getCapacity();

            //Checks to see if route is overloaded.
            if (load > capacity) {
                //Adds overloaded route to ArrayList.
                overloadedRoutes.add(route);
            }
        });
        
        //Return list with all overloaded routes.
        return overloadedRoutes;
    }
    
    /**
     * Finds and returns all under loaded routes.
     * @return ArrayList containing all under loaded routes.
     */
    public List<Route> findUnderloadedRoutes() {
        // Sync List
        List<Route> underloadedRoutes = Collections.synchronizedList(
                //Creates an ArrayList for each overloaded route.
                new ArrayList<>());

        //Enters a loop for each route.
        Routes.parallelStream().forEach((route) -> { // TODO: make parallelStream
            //Checks to see if route is underloaded.
            if (route.isUnderloaded()) {
                //Adds under loaded route to ArrayList.
                underloadedRoutes.add(route);
            }
        });
        
        //Return list with all overloaded routes.
        return underloadedRoutes;    
    }

    /**
     * @return the routes
     */
    public List<Route> getRoutes() {
        return Routes;
    }

    /**
     * @param route is added to the overall list of routes
     */
    public void addRoute(Route route) {
        this.Routes.add(route);
    }

    public void calcTimeForDeparture() {
        Routes.parallelStream().forEach(route -> { // TODO: make parallelStream


            route.setTimeForDeparture(LocalTime.of(0,0,0));
        });
    }
    }
}
