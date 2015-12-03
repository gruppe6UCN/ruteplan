using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GMap.NET;

namespace WCFWrapper
{
    [DataContract()]
    public class MapRouteWrapper
    {
        [DataMember()]
        public List<Tuple<PointLatLngWrapper, String>> MapRoutes { get; private set; }

        public MapRouteWrapper(List<MapRoute> mapRoutes)
        {
            MapRoutes = new List<Tuple<PointLatLngWrapper, String>>();

            mapRoutes.ForEach(mapRoute =>
            {
                MapRoutes.Add(new Tuple<PointLatLngWrapper, String>(
                    new PointLatLngWrapper(mapRoute.Points), mapRoute.Name));
            });
        }

        public List<MapRoute> UnWrab()
        {
            List<MapRoute> mapRoutes = new List<MapRoute>();

            MapRoutes.ForEach(mapRoute =>
            {
                IEnumerable<PointLatLng> points = mapRoute.Item1.UnWrab();
                String name = mapRoute.Item2;
                mapRoutes.Add(new MapRoute(points, name));
            });
            return mapRoutes;
        }
    }
}
