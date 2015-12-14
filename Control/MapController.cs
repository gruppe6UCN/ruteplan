using System;
using System.Collections.Concurrent;
using Model;
using System.Collections.Generic;
using System.Linq;
using Database;
using GMap.NET;

namespace Control
{
    public class MapController
    {
        private static MapController instance;

        private readonly DBGeoLoc dbGeoLoc;

        private readonly ConcurrentDictionary<long, GeoLoc> geoLocs = new ConcurrentDictionary<long, GeoLoc>();

        private ConcurrentDictionary<long, List<MapRoute>> calcRoutes = new ConcurrentDictionary<long, List<MapRoute>>();

        private GMap.NET.GMaps gMap;
        public static GMap.NET.MapProviders.OpenStreetMapProvider MapProvider { get { return GMap.NET.MapProviders.OpenStreetMapProvider.Instance; } }

        public static MapController Instance {
            get { 
                if (instance == null)
                    instance = new MapController();
                return instance;
            }
        }

        private MapController() {
            dbGeoLoc = DBGeoLoc.Instance;
            gMap = GMap.NET.GMaps.Instance;
        }

        public List<MapRoute> GetCalcRoad(Route route)
        {
            if (route.Stops.Count == 0)
            {
                return null;
            }

            // Check for a pre-calculated road
            if (route.ID != 0 && calcRoutes.ContainsKey(route.ID))
                return calcRoutes[route.ID];
            else if (calcRoutes.ContainsKey(route.DefaultRoute.ID))
                return calcRoutes[route.DefaultRoute.ID];

            // Because there was not calculated road, calculate one now.
            PreCalcRoad(route);

            // Now that we have calculated a road, do a recursive call
            return GetCalcRoad(route);
        }

        public void PreCalcRoad(Route route)
        {
            List<MapRoute> map = CalcRoad(route);
            // Store for later use
            if (route.ID != 0)
                calcRoutes[route.ID] = map;
            else
                calcRoutes[route.DefaultRoute.ID] = map;
        }

        private List<MapRoute> CalcRoad(Route route)
        {
            // TODO: Dani lave en god kommentar
            PointLatLng arlaFoodHobro = new PointLatLng(56.6372393, 9.7797216);
            DeliveryStop[] sortedStops = new DeliveryStop[route.Stops.Count];

            List<MapRoute> map = new List<MapRoute>();

            double shortestDistances;
            ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> roads;

            roads = CalcDistance(arlaFoodHobro, route.Stops);
            if (roads.Count == 0)
            {
                return null;
            }
            shortestDistances = roads.Keys.Min();

            sortedStops[0] = roads[shortestDistances].Item2;
            map.Add(roads[shortestDistances].Item1);

            for (int i = 1; i < route.Stops.Count; i++)
            {
                roads = CalcDistance(sortedStops[i - 1].DefaultStop.GeoLoc.Point, route.Stops);

                shortestDistances = roads.Keys.Min();
                sortedStops[i] = roads[shortestDistances].Item2;
                map.Add(roads[shortestDistances].Item1);
            }

            return map;
        }

        private static ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> CalcDistance(PointLatLng from,
            List<DeliveryStop> stops)
        {
            ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> roads =
                new ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>>();

            MapRoute mapRoute = null;
            //Parallel.ForEach(stops, to =>
            stops.ForEach(to =>
            {
                try
                {
                    lock (MapProvider)
                    {
                        mapRoute = MapProvider.GetRoute(from, to.DefaultStop.GeoLoc.Point, false, false, 15);
                    }

                    // TODO: What to do if there is 2 distance with the same value?
                    roads.AddOrUpdate(mapRoute.Distance, new Tuple<MapRoute, DeliveryStop>(mapRoute, to),
                        (d, roudAndStop) => roudAndStop);
                }
                catch (NullReferenceException e)
                {
                    //Console.WriteLine(e); //TODO: Find out why it is happens
                }
            });

            return roads;
        }

        public static MapRoute ShortestDistancesTo(DeliveryStop from, Route route)
        {
            ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> roads =
                CalcDistance(from.DefaultStop.GeoLoc.Point, route.Stops);

            double shortestDistances = roads.Keys.Min();

            return roads[shortestDistances].Item1;
        }

        public void AddGeoLog(DefaultDeliveryStop stop)
        {
            stop.GeoLoc = dbGeoLoc.getGeoLoc(stop.GeoLocID);
        }
    }
}
