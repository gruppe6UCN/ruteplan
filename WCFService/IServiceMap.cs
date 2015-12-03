using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using GMap.NET;
using Model;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceMap" in both code and config file together.
    [ServiceContract]
    public interface IServiceMap
    {
        //List<MapRoute> GetRoadMap(Route route);
        [OperationContract]
        MapRouteWrapper GetRoadMap(Route route);
    }

    [DataContract()]
    public class MapRouteWrapper
    {
        [DataMember()]
        public List<Tuple<PointLatLngWrapper, String>> MapRoutes { get; private set; }

        public MapRouteWrapper(List<MapRoute> mapRoutes)
        {
            MapRoutes = new List<Tuple<PointLatLngWrapper,String>>();

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
                mapRoutes.Add(new MapRoute(points,name));
            });
            return mapRoutes;
        }
    }

    [DataContract()]
    public class PointLatLngWrapper
    {
        [DataMember()]
        public List<Tuple<double, double>> Points { get; private set; }

        public PointLatLngWrapper(IEnumerable<PointLatLng> points)
        {
            Points = new List<Tuple<double, double>>();

            foreach (PointLatLng point in points)
            {
                Points.Add(new Tuple<double,double>(point.Lat, point.Lng));
            }
        }

        public IEnumerable<PointLatLng> UnWrab()
        {
            List<PointLatLng> points = new List<PointLatLng>();
            Points.ForEach(point =>
            {
                double Lat = point.Item1;
                double Lng = point.Item2;
                points.Add(new PointLatLng(Lat, Lng));
            });
            return points;
        }
    }
}
