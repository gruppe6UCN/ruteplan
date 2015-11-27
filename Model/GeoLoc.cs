using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using GMap.NET;

namespace Model
{
    [DataContract()]
    public class GeoLoc
    {
        [DataMember()]
        public long ID { get; private set; }
        [DataMember()]
        public GeoCoordinate Location { get; private set; }

        public GeoLoc(long id, double latitude, double longitude)
        {
            this.ID = id;
            this.Location = new GeoCoordinate(latitude, longitude);
        }

        public double FliedDistance(GeoLoc geoLoc)
        {
            return Location.GetDistanceTo(geoLoc.Location) / 1000; //Kilometer
        }

        public PointLatLng Point {
            get { return new PointLatLng(Location.Latitude, Location.Longitude); }
        }
    }
}