using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultRoute
    {
        public ulong ID { get; set; }
        public double TrailerType { get; private set; }
        public Boolean ExtraRoute { get; private set; }

        public DefaultRoute(ulong id, double trailerType, Boolean extraRoute)
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

      
        /**
         * @return the extraRoute
         */
        public Boolean isExtraRoute()
        {
            return ExtraRoute;
        }
    }
}
