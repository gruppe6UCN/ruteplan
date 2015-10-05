using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Customer
    {
        public long ID { get; private set; }
        public String StreetName { get; private set; }
        public String StreetNo { get; private set; }
        public int Zipcode { get; private set; }
        public String City { get; private set; }
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
