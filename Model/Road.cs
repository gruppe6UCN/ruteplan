using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Road
    {
        public long from { get; private set; }
        public long to { get; private set; }
        public double distance { get; private set; }
        public DateTime time { get; private set; }

        public Road(long from_ID, long to_ID, double distance, DateTime time)
        {
            this.from = from_ID;
            this.to = to_ID;
            this.distance = distance;
            this.time = time;
        }
    }

}
