using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using Model;

namespace Control
{
    public class DefaultRouteController
    {
        public DBDefaultRoute DbDefaultRoute { get; private set; }
        private static DefaultRouteController instance;

        /**
         * Private constructor for singleton.
         */
        private DefaultRouteController() {
        DbDefaultRoute = DBDefaultRoute.Instance;
    }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DefaultRouteController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DefaultRouteController();
                return instance;
            }
        }

        /**
         * Gets an ArrayList of all defaultRoutes from the database.
         * @return List of all defaultRoutes.
         */
        public List<DefaultRoute> GetDefaultRoutes()
        {
            //Gets a list of all defaultRoutes.
            List<DefaultRoute> list = DbDefaultRoute.DefaultRoutes();

            //Returns the list.
            return list;
        }

        public void store(DefaultRoute defaultRoute)
        {
            DbDefaultRoute.store(defaultRoute);
        }
    }
}
