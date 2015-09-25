using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultRoute
    {
        public long id { get; private set; }
        public TrailerType trailerType { get; private set; }
        public Boolean extraRoute { get; private set; }

        public DefaultRoute(long id, TrailerType trailerType, Boolean extraRoute)
        {
            this.id = id;
            this.trailerType = trailerType;
            this.extraRoute = extraRoute;
        }

        public DefaultRoute(TrailerType trailerType, Boolean extraRoute)
        {
            this.trailerType = trailerType;
            this.extraRoute = extraRoute;
        }

      
        /**
         * @return the extraRoute
         */
        public Boolean isExtraRoute()
        {
            return extraRoute;
        }
    }
}
