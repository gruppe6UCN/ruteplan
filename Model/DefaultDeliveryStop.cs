using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract()]
    public class DefaultDeliveryStop
    {
        [DataMember()]
        public long ID { get; private set; }
        [DataMember()]
        public List<Customer> Customers { get; set; }
        [DataMember()]
        private long _geoLocID;
        [DataMember()]
        public int? SequenceNbr { get; private set; }

        [DataMember()]
        public long GeoLocID
        {
            get { return GeoLoc == null ? _geoLocID : GeoLoc.ID; }
        }

        [DataMember()]
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
