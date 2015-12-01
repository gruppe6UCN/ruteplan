using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using GMap.NET;
using Model;

namespace Control
{
    public class MapController
    {
        private static MapController instance;

        private readonly DBGeoLoc dbGeoLoc;

        private readonly ConcurrentDictionary<long, GeoLoc> geoLocs = new ConcurrentDictionary<long, GeoLoc>();

        private static ConcurrentDictionary<long, List<MapRoute>> calcRoutes = new ConcurrentDictionary<long, List<MapRoute>>();

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

        public static List<MapRoute> CalcRoute(Route route)
        {
            if (route.ID != 0 || calcRoutes.ContainsKey(route.ID))
            {
                return calcRoutes[route.ID];
            }
            else if (calcRoutes.ContainsKey(route.DefaultRoute.ID))
            {
                return calcRoutes[route.DefaultRoute.ID];
            }

            PointLatLng arlaFoodHobro = new PointLatLng(56.6372393, 9.7797216);
            DeliveryStop[] sortedStops = new DeliveryStop[route.Stops.Count];

            List<MapRoute> map = new List<MapRoute>();

            double shortestDistances;
            ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> roads;

            roads = CalcDistance(arlaFoodHobro, route.Stops);
            shortestDistances = roads.Keys.Min();
            sortedStops[0] = roads[shortestDistances].Item2;
            map.Add(roads[shortestDistances].Item1);

            try
            {
                for (int i = 1; i < route.Stops.Count; i++)
                {
                    roads = CalcDistance(sortedStops[i - 1].DefaultStop.GeoLoc.Point, route.Stops);

                    shortestDistances = roads.Keys.Min();
                    sortedStops[i] = roads[shortestDistances].Item2;
                    map.Add(roads[shortestDistances].Item1);
                }
            }
            catch (System.AggregateException e)
            {

                Console.WriteLine(e);
            }


            if (route.ID != 0)
            {
                calcRoutes[route.ID] = map;
            }
            else
            {
                calcRoutes[route.DefaultRoute.ID] = map;
            }

            return map;
        }

        public static ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> CalcDistance(PointLatLng from, List<DeliveryStop> stops)
        {
            try
            {
                ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>> roads = new ConcurrentDictionary<double, Tuple<MapRoute, DeliveryStop>>();

                Parallel.ForEach(stops, to =>
                {
                    MapRoute mapRoute = MapProvider.GetRoute(from,
                        to.DefaultStop.GeoLoc.Point, false, false, 15);

                    // TODO: What to do if there is 2 distance with the same value?
                    roads.AddOrUpdate(mapRoute.Distance, new Tuple<MapRoute, DeliveryStop>(mapRoute, to),
                        (d, roudAndStop) => roudAndStop);
                });

                return roads;
            }
            catch (AggregateException e)
            {

                Console.WriteLine(e);
            }

            return null;
        }

        public static MapRoute ShortestDistancesTo(DeliveryStop from, Route route)
        {
            List<GeoLoc> geoLocs = new List<GeoLoc>();
            route.Stops.ForEach(stop => {geoLocs.Add(stop.DefaultStop.GeoLoc);});
            ConcurrentDictionary<double, MapRoute> roads = new ConcurrentDictionary<double, MapRoute>();

            Parallel.ForEach(route.Stops, to =>
            {
                MapRoute mapRoute = MapProvider.GetRoute(from.DefaultStop.GeoLoc.Point,
                    to.DefaultStop.GeoLoc.Point, false, false, 15);

                    // TODO: What to do if there is 2 distance with the same value?
                    roads.AddOrUpdate(mapRoute.Distance, mapRoute, (d, route1) => route1);
            });

            double shortestDistances = roads.Keys.Min();

            return roads[shortestDistances];
        }

        public void AddGeoLog(DefaultDeliveryStop stop)
        {
            stop.GeoLoc = dbGeoLoc.getGeoLoc(stop.GeoLocID);
        }
    }
}
