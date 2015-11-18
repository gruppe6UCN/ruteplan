using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private ConcurrentQueue<Route> overloadedRoutes;
        private ConcurrentQueue<Route> underloadedRoutes;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private OptimizeController() {
            RouteCtr = RouteController.Instance;
            MapCtr = MapController.Instance;
            LogCtr = LogController.Instance;

            // Sync Queue
            removedStops = new ConcurrentQueue<DeliveryStop>();
        }

        /// <summary>
        /// Singleton method. Returns the Instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
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
            //LogCtr.StatusLog("searching for over- and underloaded");

            //overloadedRoutes = RouteCtr.FindOverloadedRoutes();
            //underloadedRoutes = RouteCtr.FindUnderloadedRoutes();

            //LogCtr.StatusLog("Found all over- and under-loaded routes");

            ////Checks to see if there is overloadedRoutes.
            //if (overloadedRoutes.Count > 0) {

            //    //Enters a loop for each overloaded route.
            //    Parallel.ForEach(overloadedRoutes, overloadedRoute => {

            //        //Finds overloaded amount.
            //        double overloadAmount = overloadedRoute.GetLoadForTrailer() - overloadedRoute.GetCapacity();

            //        //Removes deliveryStops from route, until it's not overloaded, using a greedy algorithm.
            //        while (overloadAmount > 0) {

            //            //Finds the most/best overloaded stop.
            //            DeliveryStop best = FindBestOverloadedStop(overloadedRoute);

            //            //Removes stop from route.
            //            overloadedRoute.getStops().remove(best);

            //            //Adds stop to ArrayList.
            //            removedStops.add(best);

            //            //Decrements overload.
            //            overloadAmount -= best.GetSizeOfTransportUnits();
            //        }
            //    });
            //    LogCtr.StatusLog(String.format("There is %d delivery stop there need to be moved to another routes", overloadedRoutes.size()));

            //    //Waiting for the thread which finds routes if it isn't done
            //    try {
            //        prelaodMap.join();
            //    } catch (InterruptedException e) {
            //        e.printStackTrace();
            //    }
            //    LogCtr.StatusLog("Map module Loaded");

            //    //Enter a loop for each stop removed.
            //    removedStops.stream().forEach((removedStop) -> {
            //        //Sync HashMap
            //        Map<Double, Route> nearRoutes = Collections.synchronizedMap(
            //                //HashMap containing all routes near removedStop with distance as the key.
            //                new HashMap<>());

            //        //Finds the geoLoc for current removedStop.
            //        GeoLoc geoLocRemovedStop = mapController.findGeoLoc(removedStop);

            //        LogCtr.StatusLog("Searching for routes at a close enough distance, og with enough free space to add the stop");
            //        //Enters a loop for each underloadedRoute, to check if one of them is near stop.
            //        underloadedRoutes.parallelStream().forEach((underloadedRoute) -> {  // TODO: make parallelStream
            //            //Checks if there is space to removedStop on this Route
            //            double newLoad = removedStop.getSizeOfTransportUnits() + underloadedRoute.getLoadForTrailer();
            //            if (newLoad <= underloadedRoute.getCapacity()) {

            //                //Sync HashMap
            //                Map<Double, DeliveryStop> distanceForStops = Collections.synchronizedMap
            //                        (new HashMap<>());

            //                underloadedRoute.getStops().parallelStream().forEach(deliveryStop -> { // TODO: make parallelStream
            //                    //Gets the geoLoc, and store the distance for the geoLoc.
            //                    GeoLoc geoLacBelongingToRoute = mapController.findGeoLoc(deliveryStop);
            //                    double distance = mapController.getShortestDistance(geoLocRemovedStop, geoLacBelongingToRoute);

            //                    distanceForStops.put(distance, deliveryStop);
            //                });

            //                // The shortest distance (HashMap key) for this Route
            //                Double distance = distanceForStops.keySet().stream().sorted().findFirst().get();

            //                //Checks to see if within a acceptable distance.
            //                if (distance < 25) {
            //                    nearRoutes.put(distance, underloadedRoute);
            //                }
            //            }
            //        });

            //        if (nearRoutes.size() > 0) {
            //            // The shortest distance (HashMap key) for all the Routes
            //            Double distance = nearRoutes.keySet().stream().sorted().findFirst().get();

            //            //Move stop to this route.
            //            Route underloadedRoute = nearRoutes.get(distance);
            //            underloadedRoute.addDeliveryStop(removedStop);
            //            if (!underloadedRoute.isUnderloaded()) {
            //                underloadedRoutes.remove(underloadedRoute);
            //            }

            //            LogCtr.StatusLog("Added delivery stop to route");
            //        } else {

            //            //Make a new route.
            //            Route newRoute = newRoute(removedStop);
            //            routeController.addRoute(newRoute);
            //            if (newRoute.isUnderloaded()) {
            //                underloadedRoutes.add(newRoute);
            //            }

            //            LogCtr.StatusLog("Created a new route for delivery stop");
            //        }
            //    });

            //    //Clears ArrayList
            //    removedStops.clear();

            //    routeController.calcTimeForDeparture();
            //}
        }


        /**
         * Makes a new route and add a delivery stop.
         *
         * @param deliveryStop
         * @return returns the new route.
         */
        private static Route ExtraRoute(DeliveryStop deliveryStop) {
            //Create default route.
            DefaultRoute defaultRoute = new DefaultRoute(trailerType: TrailerType.STOR, extraRoute: true);

            //Create route.
            Route route = new Route(defaultRoute, DateTime.Now);

            //Add stops.
            route.Stops.Add(deliveryStop);

            //Return extra route.
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

        public static List<DeliveryStop> FindBestOverloadedStop(Route route, ConcurrentBag<Route> Routes)
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

            //Returns the best load.
            return removedStops;
        }
    }
}
