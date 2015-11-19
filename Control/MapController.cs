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

        public static List<Road> ShortestDistances(GeoLoc from, List<GeoLoc> geoLocs)
        {
            List<Road> roads = new List<Road>();
            geoLocs.ForEach(to =>
            {
                if(!Equals(from, to))
                {
                    MapRoute route = MapProvider.GetRoute(from.Point, to.Point, false, false, 15);

                    if (from.FliedDistance(to)*1.25 > route.Distance || route.Distance < 4)
                    {
                        roads.Add(new Road(to, from, route.Distance, new TimeSpan(0, 0, 0)));
                    }
                }
            });

            roads.Sort((road1, road2) =>
            {
                if (road1.Distance > road2.Distance)
                    return 1;
                if (road1.Distance < road2.Distance)
                    return -1;
                return 0;
            });

            return roads;
        }
    }
}
