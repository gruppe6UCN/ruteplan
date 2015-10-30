using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class RouteController
    {
            private DeliveryStopController deliveryStopController;
    private DefaultRouteController defaultRouteController;
    private DefaultDeliveryStopController defaultDeliveryStopController;
    private LogController log;
    private DBRoute dbRoute;
    private static RouteController instance;
    private List<Route> routes = Collections.synchronizedList(new ArrayList<>()) ;

    /**
     * Private constructor for singleton.
     */
    private RouteController() {
        deliveryStopController = DeliveryStopController.getInstance();
        defaultRouteController = DefaultRouteController.getInstance();
        defaultDeliveryStopController = DefaultDeliveryStopController.getInstance();
        log = LogController.getInstance();
        try {
            dbRoute = DBRoute.getInstance();
        } catch (ClassNotFoundException e) {
            log.StatusLog("Your are missing the database client module");
            e.printStackTrace();
        } catch (SQLException e) {
            log.StatusLog("Sql failed");
        }
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static RouteController getInstance() {
        if (instance == null) {
            instance = new RouteController();
        }

        return instance;
    }

    /**
     * Imports all routes from database.
     */
    public void importRoutes(LocalDate date) {
        // Remove old data
        routes.clear();

        // Sync List
        List<DefaultRoute> listDefaultRoutes = Collections.synchronizedList(
                //Gets a list of all defaultRoutes.
                defaultRouteController.getDefaultRoutes());
        log.StatusLog("Loaded defualt routes");

        //create a route for each defaultRoute
        listDefaultRoutes.parallelStream().forEach((defaultRoute -> { // TODO: make parallelStream
            //Creates new routes for each defaultRoute.
            Route route = new Route(defaultRoute, date);
            log.StatusLog("Creates new route, base on default route " + defaultRoute.getID());

            //Sync then adding
            synchronized (listDefaultRoutes) {
                //Creates Delivery Stops
                deliveryStopController.addDeliveryStops(
                        route,
                        //Gets a list of all stops for the defaultRoute.
                        defaultDeliveryStopController.getDefaultDeliveryStops(defaultRoute)
                );
            }
            log.StatusLog("Created new route from default route " + defaultRoute.getID());

            routes.add(route);
        }));
    }

    /**
     * Exports all data to database.
     */
    public void exportData() {

        routes.forEach(route -> {
            if (route.getDefaultRoute().isExtraRoute()) {
                defaultRouteController.store(route.getDefaultRoute());

            }
            dbRoute.storeRoute(route);
            deliveryStopController.storeDeliveryStops(route);

            log.StatusLog(String.format("Exported %sroute %d to database",
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
        routes.parallelStream().forEach((route) -> { // TODO: make parallelStream

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
        routes.parallelStream().forEach((route) -> { // TODO: make parallelStream
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
        return routes;
    }

    /**
     * @param route is added to the overall list of routes
     */
    public void addRoute(Route route) {
        this.routes.add(route);
    }

    public void calcTimeForDeparture() {
        routes.parallelStream().forEach(route -> { // TODO: make parallelStream


            route.setTimeForDeparture(LocalTime.of(0,0,0));
        });
    }
    }
}
