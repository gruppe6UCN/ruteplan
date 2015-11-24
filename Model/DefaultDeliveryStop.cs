using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultDeliveryStop
    {
        public long ID { get; private set; }
        public List<Customer> Customers { get; set; }
        private long _geoLocID;
        public int? SequenceNbr { get; private set; }

        public long GeoLocID
        {
            get { return GeoLoc == null ? _geoLocID : GeoLoc.ID; }
        }

        public GeoLoc GeoLoc { get; set; }

        public DefaultDeliveryStop(long ID, long geoLocId)
        {
            this.ID = ID;
            this._geoLocID = geoLocId;
        }

        public DefaultDeliveryStop(long ID, long geoLocId, int? SequenceNbr)
        {
            this.ID = ID;
            this._geoLocID = geoLocId;
            this.SequenceNbr = SequenceNbr;
        }
    }
}
