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
    public List<Route> Routes { get; private set; }
    private static RouteController instance;

    /**
     * Private constructor for singleton.
     */
    private RouteController() {
        DeliveryStopCtr = DeliveryStopController.Instance;
        DefaultRouteCtr = DefaultRouteController.Instance;
        DefaultDeliveryStopCtr = DefaultDeliveryStopController.Instance;
        LogCtr = LogController.Instance;
        DbRoute = DBRoute.Instance;
        Routes = new List<Route>();
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
        
        //Loads default routes.
        Routes.Clear();
        List<DefaultRoute> listDefaultRoues = DefaultRouteCtr.GetDefaultRoutes();
        LogCtr.StatusLog("Loaded Default Routes");

        //Creates a route for each default route.
        Parallel.ForEach(listDefaultRoues, defaultRoute =>
        {
            //Creates the route.
            Route route = new Route(defaultRoute, date, date);
            LogCtr.StatusLog("Creating new route, based on default route " + defaultRoute.ID);
            
            //Syncronize then add stops.
            lock (listDefaultRoues)
            {
                DeliveryStopCtr.addDeliveryStops(route, DefaultDeliveryStopCtr.GetDefaultDeliveryStops(defaultRoute));
            }
           
            //Updates log and adds route.
            LogCtr.StatusLog("Created new route from default route " + defaultRoute.ID);
            Routes.Add(route);
        });
    }

    /**
     * Exports all data to database.
     */
    public void ExportData() {

        //Enters a loop for each route.
        foreach (Route route in Routes)
        {
            //Checks if extra route.
            if (route.DefaultRoute.ExtraRoute)
            {
                //Saves extra route.
                DefaultRouteCtr.store(route.DefaultRoute);
            }

            //Stores routes and stops to database.
            DbRoute.storeRoute(route);
            DeliveryStopCtr.StoreDeliveryStops(route);
            
            //Updates log.
            LogCtr.StatusLog(string.Format("Exported {0} route {1} to database",
                route.DefaultRoute.ExtraRoute ? "Extra " : "", 
                route.ID
                ));
        }
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
