using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract()]
    public class DefaultRoute
    {
        [DataMember()]
        public long ID { get; set; }
        [DataMember()]
        public double TrailerType { get; set; }
        [DataMember()]
        public Boolean ExtraRoute { get; private set; }

        public DefaultRoute(long id, double trailerType, Boolean extraRoute)
        {
            this.ID = id;
            this.TrailerType = trailerType;
            this.ExtraRoute = extraRoute;
        }

        public DefaultRoute(double trailerType, Boolean extraRoute)
        {
            this.TrailerType = trailerType;
            this.ExtraRoute = extraRoute;
        }
    }
}
