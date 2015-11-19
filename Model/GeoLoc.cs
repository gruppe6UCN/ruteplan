using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    public class GeoLoc // : ISerializable
    {
        public long ID { get; private set; }
        public Point Location { get; private set; }

        public GeoLoc(long id, double latitude, double longitude)
        {
            this.ID = id;
            this.Location = new Point(latitude, longitude);
        }

    }
}