using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Road
    {
        public long From { get; private set; }
        public long To { get; private set; }
        public double Distance { get; private set; }
        public TimeSpan Time { get; private set; }

        private GeoLoc _fromGeoLoc;
        public GeoLoc FromGeoLog
        {
            get { return _fromGeoLoc; }
            set
            {
                From = value.ID;
                _fromGeoLoc = value;
            }
        }
        private GeoLoc _toGeoLoc;
        public GeoLoc ToGeoLog
        {
            get { return _toGeoLoc; }
            set
            {
                To = value.ID;
                _toGeoLoc = value;
            }
        }

        public Road(long from_ID, long to_ID, double distance, TimeSpan time)
        {
            this.From = from_ID;
            this.To = to_ID;
            this.Distance = distance;
            this.Time = time;
        }
        public Road(GeoLoc from, GeoLoc to, double distance, TimeSpan time)
        {
            this.FromGeoLog = from;
            this.ToGeoLog = to;
            this.From = FromGeoLog.ID;
            this.To = ToGeoLog.ID;
            this.Distance = distance;
            this.Time = time;
        }

        public bool Equals(Road road)
        {
            bool from = false;
            bool to = false;

            if (FromGeoLog == null)
            {
                if (road.FromGeoLog == null)
                {
                    from = From == road.From;
                }
                else
                {
                    from = From == road.FromGeoLog.ID;
                }
            }
            else
            {
                if (road.FromGeoLog == null)
                {
                    from = FromGeoLog.ID == road.From;
                }
                else
                {
                    from = FromGeoLog.ID == road.FromGeoLog.ID;
                }
            }

            if (ToGeoLog == null)
            {
                if (road.ToGeoLog == null)
                {
                    to = To == road.To;
                }
                else
                {
                    to = To == road.ToGeoLog.ID;
                }
            }
            else
            {
                if (road.ToGeoLog == null)
                {
                    to = ToGeoLog.ID == road.To;
                }
                else
                {
                    to = ToGeoLog.ID == road.ToGeoLog.ID;
                }
            }
            return from && to;
        }
    }

}
