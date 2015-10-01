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
        public DateTime Time { get; private set; }

        public Road(long from_ID, long to_ID, double distance, DateTime time)
        {
            this.From = from_ID;
            this.To = to_ID;
            this.Distance = distance;
            this.Time = time;
        }
    }

}
