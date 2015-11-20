using System;
using System.Collections.Concurrent;
using Model;
using Server.Database;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Control
{
    public class MapController
    {
        private static MapController instance;

        private readonly DBGeoLoc dbGeoLoc;
        private readonly DBRoad dbRoad;

        private readonly ConcurrentDictionary<long, GeoLoc> geoLocs = new ConcurrentDictionary<long, GeoLoc>();

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
            dbRoad = DBRoad.Instance;
            gMap = GMap.NET.GMaps.Instance;
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
