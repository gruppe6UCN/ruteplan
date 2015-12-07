using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GMap.NET;

namespace WCFWrapper
{

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
                Points.Add(new Tuple<double, double>(point.Lat, point.Lng));
            }
        }

        public IEnumerable<PointLatLng> Unwrap()
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
