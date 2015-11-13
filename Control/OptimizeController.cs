using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class OptimizeController
    {
           private RouteController routeController;
    private MapController mapController;
    private LogController log;
    private static OptimizeController instance;
    private List<DeliveryStop> removedStops;

    List<Route> overloadedRoutes;
    List<Route> underloadedRoutes;

    /**
     * Private constructor for singleton.
     */
    private OptimizeController() {
        routeController = RouteController.getInstance();
        mapController = MapController.getInstance();
        log = LogController.getInstance();
        // Sync List
        removedStops = Collections.synchronizedList(new ArrayList<>());
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static OptimizeController getInstance() {
        if (instance == null) {
            instance = new OptimizeController();
        }

        return instance;
    }

    /**
     * Optimizes all imported routes.
     * @param rowData
     */
    public void optimize(Vector rowData) {
        optimize();

        List<Route> routes = routeController.getRoutes();

        routes.forEach(route -> {
            Vector row = new Vector();
            row.addElement(route.getDefaultRoute().isExtraRoute() ? "NONE" : String.format("%03d", route.getDefaultRoute().getID()));
            row.addElement(route.getStops().size());
            row.addElement(String.format("%.1f / %.1f", route.getLoadForTrailer(), route.getCapacity()));
            row.addElement(String.format("%02d:%02d", route.getTimeForDeparture().getHour(), route.getTimeForDeparture().getMinute()));
            row.addElement(route.getDefaultRoute().isExtraRoute() ? "Yes" : "No");
            rowData.add(row);
        });
    }

    /**
     * Optimizes all imported routes.
     */
    public void optimize() {
        log.StatusLog("searching for over- and underloaded");

        // Declaring an anonymous class which implements the interface Runnable
        Thread preloadRoutes = new Thread(new Runnable() {
            public void run() {
                overloadedRoutes = routeController.findOverloadedRoutes();
                underloadedRoutes = routeController.findUnderloadedRoutes();
            }
        });
        preloadRoutes.start();


        log.StatusLog("Loading map module...");
        Thread prelaodMap = new Thread(new Runnable() {
            public void run() {
                mapController.generateMap();
            }
        });
        prelaodMap.start();

        //Waiting for the thread which finds routes if it isn't done
        try {
            preloadRoutes.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        log.StatusLog("Found all over- and underloaded routes");

        //Checks to see if there is overloadedRoutes.
        if (overloadedRoutes.size() >= 1) {

            //Enters a loop for each overloaded route.
            overloadedRoutes.parallelStream().forEach((overloadedRoute) -> { // TODO: make parallelStream

                //Finds overloaded amount.
                double overloadAmount = overloadedRoute.getLoadForTrailer() - overloadedRoute.getCapacity();

                //Removes deliveryStops from route, until it's not overloaded, using a greedy algorithm.
                while (overloadAmount > 0) {

                    //Finds the most/best overloaded stop.
                    DeliveryStop best = findBestOverloadedStop(overloadedRoute, overloadAmount);

                    //Removes stop from route.
                    overloadedRoute.getStops().remove(best);

                    //Adds stop to ArrayList.
                    removedStops.add(best);

                    //Decrements overload.
                    overloadAmount -= best.getSizeOfTransportUnits();
                }
            });
            log.StatusLog(String.format("There is %d delivery stop there need to be moved to another routes", overloadedRoutes.size()));

            //Waiting for the thread which finds routes if it isn't done
            try {
                prelaodMap.join();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            log.StatusLog("Map module Loaded");

            //Enter a loop for each stop removed.
            removedStops.stream().forEach((removedStop) -> {
                //Sync HashMap
                Map<Double, Route> nearRoutes = Collections.synchronizedMap(
                        //HashMap containing all routes near removedStop with distance as the key.
                        new HashMap<>());

                //Finds the geoLoc for current removedStop.
                GeoLoc geoLocRemovedStop = mapController.findGeoLoc(removedStop);

                log.StatusLog("Searching for routes at a close enough distance, og with enough free space to add the stop");
                //Enters a loop for each underloadedRoute, to check if one of them is near stop.
                underloadedRoutes.parallelStream().forEach((underloadedRoute) -> {  // TODO: make parallelStream
                    //Checks if there is space to removedStop on this Route
                    double newLoad = removedStop.getSizeOfTransportUnits() + underloadedRoute.getLoadForTrailer();
                    if (newLoad <= underloadedRoute.getCapacity()) {

                        //Sync HashMap
                        Map<Double, DeliveryStop> distanceForStops = Collections.synchronizedMap
                                (new HashMap<>());

                        underloadedRoute.getStops().parallelStream().forEach(deliveryStop -> { // TODO: make parallelStream
                            //Gets the geoLoc, and store the distance for the geoLoc.
                            GeoLoc geoLacBelongingToRoute = mapController.findGeoLoc(deliveryStop);
                            double distance = mapController.getShortestDistance(geoLocRemovedStop, geoLacBelongingToRoute);

                            distanceForStops.put(distance, deliveryStop);
                        });

                        // The shortest distance (HashMap key) for this Route
                        Double distance = distanceForStops.keySet().stream().sorted().findFirst().get();

                        //Checks to see if within a acceptable distance.
                        if (distance < 25) {
                            nearRoutes.put(distance, underloadedRoute);
                        }
                    }
                });

                if (nearRoutes.size() > 0) {
                    // The shortest distance (HashMap key) for all the Routes
                    Double distance = nearRoutes.keySet().stream().sorted().findFirst().get();

                    //Move stop to this route.
                    Route underloadedRoute = nearRoutes.get(distance);
                    underloadedRoute.addDeliveryStop(removedStop);
                    if (!underloadedRoute.isUnderloaded()) {
                        underloadedRoutes.remove(underloadedRoute);
                    }

                    log.StatusLog("Added delivery stop to route");
                } else {

                    //Make a new route.
                    Route newRoute = newRoute(removedStop);
                    routeController.addRoute(newRoute);
                    if (newRoute.isUnderloaded()) {
                        underloadedRoutes.add(newRoute);
                    }

                    log.StatusLog("Created a new route for delivery stop");
                }
            });

            //Clears ArrayList
            removedStops.clear();

            routeController.calcTimeForDeparture();
        }
    }


    /**
     * Makes a new route and add a delivery stop.
     *
     * @param deliveryStop
     * @return returns the new route.
     */
    private Route newRoute(DeliveryStop deliveryStop) {

        //Create default route.
        TrailerType trailerType = TrailerType.STOR;
        boolean extraRoute = true;

        DefaultRoute defaultRoute = new DefaultRoute(trailerType, extraRoute);

        //Create route.
        Route route = new Route(defaultRoute, LocalDate.now());

        //Add stops.
        route.addDeliveryStop(deliveryStop);

        //Return route.
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
    private DeliveryStop findBestOverloadedStop(Route route, double overload) {

        //Finds an initial stops for comparison.
        DeliveryStop biggest = route.getStops().get(0);
        DeliveryStop best = biggest;

        //Boolean to check if cap is exceeded.
        boolean exceedCap = false;

        //Enters a loop for each stop, to find the most overloaded stop.
        ArrayList<DeliveryStop> stops = route.getStops();
        for (DeliveryStop deliveryStop : stops) {

            //Compares the load of the deliveryStop with the load of the biggest, to find which is biggest.
            double biggestload = biggest.getSizeOfTransportUnits();
            double compareload = deliveryStop.getSizeOfTransportUnits();
            if (compareload > biggestload) {

                //Sets the biggest to the current.
                biggest = deliveryStop;
                best = biggest;

                //Checks to see if the biggest exceeds the cap.
                if (biggestload > overload) {
                    exceedCap = true;
                } else {
                    exceedCap = false;
                }
            }
        }

        //Checks to see if the biggest exceeds the limit.
        if (exceedCap) {

            //Initial stop for comparison.
            DeliveryStop smallest = route.getStops().get(0);

            //Enters a loop for each stop, to find the least overloaded stop.
            for (DeliveryStop deliveryStop : stops) {

                //Compares the load of the deliveryStop with the load of the smallest, to find which is smallest.
                double smallestload = smallest.getSizeOfTransportUnits();
                double compareload = deliveryStop.getSizeOfTransportUnits();
                if (compareload < smallestload) {

                    //Sets the smallest to current.
                    smallest = deliveryStop;
                    best = smallest;
                }
            }
        }

        //Returns the best load.
        return best;
    }
}
