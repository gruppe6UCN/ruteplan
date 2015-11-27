using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract()]
    public class Customer
    {
        [DataMember()]
        public long ID { get; private set; }
        [DataMember()]
        public String StreetName { get; private set; }
        [DataMember()]
        public String StreetNo { get; private set; }
        [DataMember()]
        public int Zipcode { get; private set; }
        [DataMember()]
        public String City { get; private set; }
        [DataMember()]
        public TimeSpan TimeOfDelivery { get; private set; }

        public Customer(long ID, String StreetName, String StreetNo, int Zipcode, String City, TimeSpan TimeOfDelivery)
        {
            this.ID = ID;
            this.StreetName = StreetName;
            this.StreetNo = StreetNo;
            this.Zipcode = Zipcode;
            this.City = City;
            this.TimeOfDelivery = TimeOfDelivery;
        }
    }
}
