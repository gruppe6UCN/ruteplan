using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract()]
    //[KnownType(typeof(Customer))]
    public class DefaultDeliveryStop
    {
        private long _geoLocID;

        [DataMember()]
        public long ID { get; private set; }
        [DataMember()]
        public List<Customer> Customers { get; set; }
        [DataMember()]
        public int? SequenceNbr { get; private set; }
        [DataMember()]
        public GeoLoc GeoLoc { get; set; }
        [DataMember()]
        public long GeoLocID
        {
            get { return GeoLoc == null ? _geoLocID : GeoLoc.ID; }
            private set { _geoLocID = value; }
        }

        public DefaultDeliveryStop(long ID, long geoLocId)
        {
            this.ID = ID;
            GeoLocID = geoLocId;
        }

        public DefaultDeliveryStop(long ID, long geoLocId, int? SequenceNbr)
        {
            this.ID = ID;
            this._geoLocID = geoLocId;
            this.SequenceNbr = SequenceNbr;
        }
    }
}
