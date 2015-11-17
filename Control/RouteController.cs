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

    /// <summary>
    /// Private singleton constructor.
    /// </summary>
    private RouteController() {
        DeliveryStopCtr = DeliveryStopController.Instance;
        DefaultRouteCtr = DefaultRouteController.Instance;
        DefaultDeliveryStopCtr = DefaultDeliveryStopController.Instance;
        LogCtr = LogController.Instance;
        DbRoute = DBRoute.Instance;
        Routes = new List<Route>();
    }

    /// <summary>
    /// Singleton method. Returns the instance of the class.
    /// </summary>
    /// <returns>Instance of class.</returns>
    public static RouteController Instance {
        get {
                if (instance == null)
                instance = new RouteController();
            return instance;
        }
    }

    /// <summary>
    /// Imports all routes from database.
    /// </summary>
    /// <param name="date">Time used in creation of routes.</param>
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

    /// <summary>
    /// Exports all routes to database. If route contains extra default route,
    /// default route is exported as well.
    /// </summary>
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
    
    /// <summary>
    /// Finds and returns all overloaded routes.
    /// </summary>
    /// <returns>List of all overloaded Routes.</returns>
    public List<Route> FindOverloadedRoutes() {
        
        //Creates a list of routes.
        List<Route> overloadedRoutes = new List<Route>();

        //Checks if each route is overloaded.
        Parallel.ForEach(overloadedRoutes, route => 
        {
            //Gets variables.
            double load = route.GetLoadForTrailer();
            double capacity = route.GetCapacity();

            //Checks if overloaded.
            if (load > capacity)
            {
                //Adds the route to list.
                overloadedRoutes.Add(route);
            }
        });

        //Return list with routes.
        return overloadedRoutes;
    }
    
    /// <summary>
    /// Finds and returns all underloaded routes.
    /// </summary>
    /// <returns>List of all underloaded routes.</returns>
    public List<Route> FindUnderloadedRoutes() {

        //Creates a list of routes.
        List<Route> underloadedRoutes = new List<Route>();

        //Checks if each route is underloaded.
        Parallel.ForEach(underloadedRoutes, route => 
        {
            //Checks if underlaoded.
            if (route.IsUnderloaded())
            {
                //Adds to list.
                underloadedRoutes.Add(route);
            }
        });

        //Return list with routes.
        return underloadedRoutes;   
    }


    /// <summary>
    /// Calculates the time for departure.
    /// </summary>
    public void calcTimeForDeparture() {




        Routes.parallelStream().forEach(route -> { // TODO: make parallelStream


            route.setTimeForDeparture(LocalTime.of(0,0,0));
        });
    }
    }
}
