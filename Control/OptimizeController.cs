using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMap.NET;
using Model;

namespace Control
{
    public class OptimizeController
    {
        public RouteController RouteCtr  { get; private set; }
        public MapController MapCtr { get; private set; }
        public LogController LogCtr { get; private set; }
        private static OptimizeController instance;

        private ConcurrentQueue<DeliveryStop> removedStops;
        private List<Route> overloadedRoutes;
        private List<Route> underloadedRoutes;
        private int count1;
        private int count2;

        // /<summary>
        // /Private singleton constructor.
        // /</summary>
        private OptimizeController() {
            RouteCtr = RouteController.Instance;
            MapCtr = MapController.Instance;
            LogCtr = LogController.Instance;
        }

        // /<summary>
        // /Singleton method. Returns the Instance of the class.
        // /</summary>
        // /<returns>Instance of class.</returns>
        public static OptimizeController Instance {
            get
            {
                if (instance == null)
                {
                    instance = new OptimizeController();
                }
                return instance;
            }
        }

        /**
         * Optimizes all imported routes.
         */
        public void Optimize() {

            count1 = 0;
            count2 = 0;

            // Sync Queue
            removedStops = new ConcurrentQueue<DeliveryStop>();

            LogCtr.StatusLog("searching for over- and underloaded");
            overloadedRoutes = RouteCtr.FindOverloadedRoutes();
            underloadedRoutes = RouteCtr.FindUnderloadedRoutes();

            LogCtr.StatusLog("Found all over- and under-loaded routes");

            // Checks to see if there is overloadedRoutes.
            if (overloadedRoutes.Count > 0) {

                // Enters a loop for each overloaded route.
                Parallel.ForEach(overloadedRoutes, overloadedRoute => {

                    // Removes deliveryStops from route, until it's no longer overloaded, using a greedy algorithm.
                    List<DeliveryStop> stops = FindAndRemoveStops(overloadedRoute, RouteCtr.Routes);
                    foreach (DeliveryStop stop in stops)
                    {
                        removedStops.Enqueue(stop);
                    }

                    count1++;
                });
                LogCtr.StatusLog(String.Format("There is %d delivery stop there need to be moved to another routes", overloadedRoutes.Count));

                // Enter a loop for each stop removed.
                foreach (DeliveryStop removedStop in removedStops)
                {
                    // The thread-safe dictionary contains all routes near removedStop with distance as the key.
                    ConcurrentDictionary<Double, Route> nearRoutes = new ConcurrentDictionary<double, Route>();

                    LogCtr.StatusLog("Searching for routes at a close enough distance, and with enough free space to add the stop");
                    // Enters a loop for each underloadedRoute, to check if one of them is near stop.
                    Parallel.ForEach(underloadedRoutes, underloadedRoute => {  // TODO: make parallelStream
                        // Checks if there is space to removedStop on this Route
                        double newLoad = removedStop.GetSizeOfTransportUnits() + underloadedRoute.GetLoadForTrailer();
                        if (newLoad <= underloadedRoute.GetCapacity()) {
                            // Finds the shortest distance from removedStop to underloadedRoute.
                            MapRoute mapRoute = MapController.ShortestDistancesTo(removedStop, underloadedRoute);
                            
                            // Checks to see if within a acceptable distance.
                            if (mapRoute.Distance < 25) {
                                nearRoutes.AddOrUpdate(mapRoute.Distance, underloadedRoute, (d, route) => route);
                            }
                        }
                    });

                    if (nearRoutes.Count > 0) {
                        // The shortest distance (HashMap key) for all the Routes
                        double distance = nearRoutes.Keys.Min();

                        // Move stop to this route.
                        Route underloadedRoute = nearRoutes[distance];
                        underloadedRoute.Stops.Add(removedStop);
                        if (!underloadedRoute.IsUnderloaded()) {
                            underloadedRoutes.Remove(underloadedRoute);
                        }

                        LogCtr.StatusLog("Added delivery stop to route");
                    } else {
                        // Make a extra route.
                        Route extraRoute = ExtraRoute(removedStop);
                        RouteCtr.Routes.Add(extraRoute);
                        if (extraRoute.IsUnderloaded()) {
                            underloadedRoutes.Add(extraRoute);
                        }

                        LogCtr.StatusLog("Created a new route for delivery stop");
                    }

                    count2++;
                }

                RouteCtr.CalcTimeForDeparture();
            }

            //Parallel.ForEach(RouteCtr.Routes, route => MapCtr.PreCalcRoad(route));
            foreach (Route route in RouteCtr.Routes)
            {
                MapCtr.PreCalcRoad(route);
            }
        }


        /**
         * Makes a new route and add a delivery stop.
         *
         * @param deliveryStop
         * @return returns the new route.
         */
        private static Route ExtraRoute(DeliveryStop deliveryStop) {
            // Create default route.
            DefaultRoute defaultRoute = new DefaultRoute(trailerType: TrailerType.STOR, extraRoute: true);

            // Create route.
            Route route = new Route(defaultRoute, DateTime.Now);

            // Add stops.
            route.Stops.Add(deliveryStop);

            // Return extra route.
            return route;
        }


        /**
         * Finds the most overloaded delivery stop in the given route, as long as it doesn't exceed the overload cap.
         * If no such stop can be found, finds the least overloaded route instead.
         *
         * @param route    the route whose stops are to be checked.
         * @param overload the overload cap to exclude stops whose load are not optimal.
         * @return the best suited deliveryStop whose load most closely fits the cap.
         */

        public static List<DeliveryStop> FindAndRemoveStops(Route route, ConcurrentBag<Route> Routes)
        {
            List<DeliveryStop> removedStops = new List<DeliveryStop>();

            // Sort delivery stops after its load size, with the biggeste load first
            DeliveryStop[] stops = route.Stops.ToArray();
            Array.Sort(stops, (stop1, stop2) =>
            {
                if (stop1.GetSizeOfTransportUnits() < stop2.GetSizeOfTransportUnits())
                    return 1;
                if (stop1.GetSizeOfTransportUnits() > stop2.GetSizeOfTransportUnits())
                    return -1;
                return 0;
            });

            // Removes all delivery stops from route
            route.Stops.Clear();

            for(int x = 0; x < stops.Length; )
            {
                // Check if the size of the trasnsport units is more then there is space to in the trailer
                if (route.DefaultRoute.TrailerType >= stops[x].GetSizeOfTransportUnits())
                {
                    // Checks if there is enough space in the Trailer else adds the stop to removedStops
                    if (route.GetCapacity() >= route.GetLoadForTrailer() + stops[x].GetSizeOfTransportUnits())
                    {
                        route.Stops.Add(stops[x]);
                    }
                    else
                    {
                        removedStops.Add(stops[x]);
                    }
                    x += 1;
                }
                else // Create a extra route for this stop, becasuse there is not enough space in the trailer
                {
                    DeliveryStop newStop = new DeliveryStop(stops[x].DefaultStop);
                    Route extraRoute = ExtraRoute(newStop);

                    // Sort transport units after there size, with the biggeste first
                    stops[x].TransportUnits.Sort((transport1, transport2) =>
                    {
                        if (transport1.UnitType < transport2.UnitType)
                            return 1;
                        if (transport1.UnitType > transport2.UnitType)
                            return -1;
                        return 0;
                    });

                    // Moves transport units to extra route until the trailer is full
                    for(int i = 0; i < stops[x].TransportUnits.Count; )
                    {
                        TransportUnit transportUnit = stops[x].TransportUnits[i];
                        if (transportUnit.UnitType + newStop.GetSizeOfTransportUnits() < extraRoute.GetCapacity())
                        {
                            stops[x].TransportUnits.RemoveAt(0);
                            newStop.TransportUnits.Add(transportUnit);
                        }
                        else
                        {
                            i += 1;
                        }
                    }

                    // Adds the extra route to the total collection of routes
                    Routes.Add(extraRoute);
                }
            }

            // Returns the best load.
            return removedStops;
        }


        /// <summary>
        /// Gives a percentage of completion as an int of 0-100 for status of optmization.
        /// </summary>
        /// <returns>Int value from 0-100</returns>
        public int GetStatus()
        {
            try
            {
                //Calculates count1
                int max1 = overloadedRoutes.Count;
                double steps1 = count1 / max1;
                double status1 = Math.Floor(steps1 * 100) / 2;

                //Calculates count2
                int max2 = removedStops.Count;
                double steps2 = count2 / max2;
                double status2 = Math.Floor(steps2 * 100) / 2;

                //Calculates total.
                double total = status1 + status2;
                return Convert.ToInt32(total);
            }
            catch (NullReferenceException) { return 0; }
            catch (DivideByZeroException) { return 0; }
        }

    }
}
